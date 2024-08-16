using Microsoft.AspNetCore.Mvc;
using Moq;
using SGT.API.Controllers;
using SGT.Application.DTOs.Users;
using SGT.Application.Interfaces;
using SGT.Domain.Enum;
using SGT.Infrastructure.Messaging.Producers.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SGT.Tests
{
    public class UserControllerTest
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<IUserProducer> _userProducerMock;
        private readonly UserController _controller;

        public UserControllerTest()
        {
            _userServiceMock = new Mock<IUserService>();
            _userProducerMock = new Mock<IUserProducer>();
            _controller = new UserController(_userServiceMock.Object, _userProducerMock.Object);
        }

        [Fact]
        public async Task Post_CriaUsuarioERetornaOkEUsuario()
        {
            var userRequestDTO = new UserRequestDTO
            {
                Name = "Bonafe",
                Email = "bonas@example.com",
                Password = "Passw0rd!"
            };
            var userResponseDTO = new UserResponseDTO
            {
                Id = 1,
                Name = "Bonafe",
                Email = "bonas@example.com"
            };
            _userServiceMock.Setup(service => service.AddUserAsync(userRequestDTO))
                .ReturnsAsync(userResponseDTO);

            var result = await _controller.Post(userRequestDTO);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedUser = Assert.IsType<UserResponseDTO>(okResult.Value);

            Assert.Equal(userResponseDTO.Name, returnedUser.Name);
            Assert.Equal(userResponseDTO.Email, returnedUser.Email);
            _userProducerMock.Verify(p => p.CreateAccountMessage(userResponseDTO), Times.Once);
        }

        [Fact]
        public async Task FindAll_RetornaOkEArrayDeUsuarios_QuandoExisteAlgumUsuario()
        {
            var users = new List<UserGetAllDTO>
            {
                new UserGetAllDTO
                {
                    Id = 1,
                    Name = "Bonafe",
                    Email = "bona1@example.com"
                },
                new UserGetAllDTO
                {
                    Id = 2,
                    Name = "Lucas",
                    Email = "bona2@example.com"
                }
            };

            _userServiceMock.Setup(service => service.GetAllUsersAsync())
                .ReturnsAsync(users);

            var result = await _controller.FindAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedUsers = Assert.IsType<List<UserGetAllDTO>>(okResult.Value);

            Assert.Equal(users.Count, returnedUsers.Count);
        }

        [Fact]
        public async Task FindById_RetornaOkEUsuario_QuandoOUsuarioExiste()
        {
            int userId = 1;
            var user = new UserResponseDTO
            {
                Id = userId,
                Name = "lu",
                Email = "luc1teste@gmail.com"
            };
            _userServiceMock.Setup(service => service.GetUserByIdAsync(userId))
                .ReturnsAsync(user);

            var result = await _controller.FindById(userId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedUser = Assert.IsType<UserResponseDTO>(okResult.Value);

            Assert.Equal(user.Id, returnedUser.Id);
            Assert.Equal(user.Name, returnedUser.Name);
            Assert.Equal(user.Email, returnedUser.Email);
            _userProducerMock.Verify(p => p.GetTimeTask(user), Times.Once);
        }

        [Fact]
        public async Task Put_AtualizaUsuario_RetornaNoContent()
        {
            var userUpdateDTO = new UserUpdateDTO
            {
                Name = "testeAtualizacao",
                Email = "updateduser@example.com"
            };
            int userId = 1;

            var result = await _controller.Put(userUpdateDTO, userId);

            Assert.IsType<NoContentResult>(result);
            _userProducerMock.Verify(p => p.UpdatedAccountMessage(userUpdateDTO), Times.Once);
        }

        [Fact]
        public async Task Delete_DeletaUsuario_RetornaOk()
        {
            int userId = 1;
            _userServiceMock.Setup(service => service.DeleteUserAsync(userId))
                .ReturnsAsync(true);

            var result = await _controller.Delete(userId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True((bool)okResult.Value);
        }
    }
}
