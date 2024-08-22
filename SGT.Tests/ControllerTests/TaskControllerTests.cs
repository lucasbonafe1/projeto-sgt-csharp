using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SGT.API.Controllers;
using SGT.Application.DTOs.Tasks;
using SGT.Application.Interfaces;
using SGT.Domain.Enum;
using SGT.Infrastructure.Messaging.Producers.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SGT.Tests.ControllerTests
{
    public class TaskControllerTest
    {
        private readonly Mock<ITaskService> _taskServiceMock;
        private readonly Mock<ITaskProducer> _taskProducerMock;
        private readonly TaskController _controller;

        public TaskControllerTest()
        {
            _taskServiceMock = new Mock<ITaskService>();
            _taskProducerMock = new Mock<ITaskProducer>();
            _controller = new TaskController(_taskServiceMock.Object, _taskProducerMock.Object);
        }

        [Fact]
        public async Task Post_CriaTasksERetornaOkETasks()
        {
            var taskRequestDTO = new TaskRequestDTO
            {
                Title = "New Task",
                Description = "Task Description",
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(5),
                Status = StatusTask.Pending,
                UserId = 1
            };
            var taskResponseDTO = new TaskResponseDTO
            {
                Title = "New Task",
                Description = "Task Description",
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(5),
                Status = StatusTask.Pending,
                UserId = 1
            };
            _taskServiceMock.Setup(service => service.AddTaskAsync(taskRequestDTO))
                .ReturnsAsync(taskResponseDTO);

            var result = await _controller.Post(taskRequestDTO);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedTask = Assert.IsType<TaskResponseDTO>(okResult.Value);

            Assert.Equal(taskResponseDTO.Title, returnedTask.Title);
            Assert.Equal(taskResponseDTO.Description, returnedTask.Description);
            Assert.Equal(taskResponseDTO.StartDate, returnedTask.StartDate);
            Assert.Equal(taskResponseDTO.EndDate, returnedTask.EndDate);
            Assert.Equal(taskResponseDTO.Status, returnedTask.Status);
            Assert.Equal(taskResponseDTO.UserId, returnedTask.UserId);

            _taskProducerMock.Verify(p => p.SendTaskMessage(taskResponseDTO), Times.Once);
        }

        [Fact]
        public async Task FindAll_RetornaOkEArrayDeTasks_QuandoExisteAlgumaTask()
        {
            var tasks = new List<TaskResponseDTO>
            {
                new TaskResponseDTO
                {
                    Title = "Task 1",
                    Description = "Description 1",
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(5),
                    Status = StatusTask.Pending,
                    UserId = 1
                },
                new TaskResponseDTO
                {
                    Title = "Task 2",
                    Description = "Description 2",
                    StartDate = DateTime.UtcNow.AddDays(1),
                    EndDate = DateTime.UtcNow.AddDays(6),
                    Status = StatusTask.Completed,
                    UserId = 2
                }
            };

            _taskServiceMock.Setup(service => service.GetAllTasksAsync())
                .ReturnsAsync(tasks);

            var result = await _controller.FindAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedTasks = Assert.IsType<List<TaskResponseDTO>>(okResult.Value);

            Assert.Equal(tasks.Count, returnedTasks.Count);
        }

        [Fact]
        public async Task FindById_RetornaOkETask_QuandoATaskExiste()
        {
            int taskId = 1;
            var task = new TaskResponseDTO
            {
                Title = "Task 1",
                Description = "Description 1",
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(5),
                Status = StatusTask.Pending,
                UserId = 1
            };
            _taskServiceMock.Setup(service => service.GetTaskByIdAsync(taskId))
                .ReturnsAsync(task);

            var result = await _controller.FindById(taskId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedTask = Assert.IsType<TaskResponseDTO>(okResult.Value);

            Assert.Equal(task.Title, returnedTask.Title);
            Assert.Equal(task.Description, returnedTask.Description);
            Assert.Equal(task.StartDate, returnedTask.StartDate);
            Assert.Equal(task.EndDate, returnedTask.EndDate);
            Assert.Equal(task.Status, returnedTask.Status);
            Assert.Equal(task.UserId, returnedTask.UserId);
        }

        [Fact]
        public async Task FindTasksByUserId_RetornaOkETasks_QuandoExisteOUserIdAtreladoAUmaTask()
        {
            int userId = 1;
            var tasks = new List<TaskResponseDTO>
            {
                new TaskResponseDTO
                {
                    Title = "User Task 1",
                    Description = "Description 1",
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(5),
                    Status = StatusTask.Pending,
                    UserId = userId
                },
                new TaskResponseDTO
                {
                    Title = "User Task 2",
                    Description = "Description 2",
                    StartDate = DateTime.UtcNow.AddDays(1),
                    EndDate = DateTime.UtcNow.AddDays(6),
                    Status = StatusTask.Completed,
                    UserId = userId
                }
            };

            _taskServiceMock.Setup(service => service.GetTasksByUserIdAsync(userId))
                .ReturnsAsync(tasks);

            var result = await _controller.FindTasksByUserId(userId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedTasks = Assert.IsType<List<TaskResponseDTO>>(okResult.Value);

            Assert.Equal(tasks.Count, returnedTasks.Count);
        }

        [Fact]
        public async Task Put_AtualizaTask_RetornaNoContent()
        {
            var taskUpdateDTO = new TaskUpdateDTO
            {
                Title = "Updated Task",
                Description = "Updated Description",
                EndDate = DateTime.UtcNow.AddDays(10),
                Status = StatusTask.Completed
            };
            int taskId = 1;

            var result = await _controller.Put(taskUpdateDTO, taskId);

            Assert.IsType<NoContentResult>(result);

            _taskProducerMock.Verify(p => p.UpdatedTaskMessage(taskUpdateDTO), Times.Once);
        }

        [Fact]
        public async Task Delete_DeletaLogicamenteTask_RetornaOk()
        {
            int taskId = 1;
            _taskServiceMock.Setup(service => service.DeleteTaskAsync(taskId))
                .ReturnsAsync(true);

            var result = await _controller.Delete(taskId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True((bool)okResult.Value);
        }
    }
}
