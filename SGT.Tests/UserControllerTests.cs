using Microsoft.AspNetCore.Mvc;
using Moq;
using SGT.API.Controllers;
using SGT.Application.DTOs.Users;
using SGT.Application.Interfaces;

namespace SGT.Tests
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _controller = new UserController(_userServiceMock.Object);
        }

        [Fact]
        public async Task Post_CriaUser_RetornaOkEUser()
        {
            // Arrange
            var userRequestDTO = new UserRequestDTO
            {
                Name = "Lucas",
                PhoneNumber = "(22) 1234567890",
                Email = "l@example.com",
                Password = "Password123!"
            };
            var userResponseDTO = new UserResponseDTO
            {
                Id = 1,
                Name = "Lucas",
                PhoneNumber = "(22) 1234567890",
                Email = "l@example.com",
                AccountCreationDate = DateTime.UtcNow
            };
            _userServiceMock.Setup(service => service.AddUserAsync(userRequestDTO))
                .ReturnsAsync(userResponseDTO);

            var result = await _controller.Post(userRequestDTO);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedUser = Assert.IsType<UserResponseDTO>(okResult.Value);

            Assert.Equal(userResponseDTO.Name, returnedUser.Name);
            Assert.Equal(userResponseDTO.PhoneNumber, returnedUser.PhoneNumber);
            Assert.Equal(userResponseDTO.Email, returnedUser.Email);
        }

        [Fact]
        public async Task FindAll_RetornaOkEListaUsers_QuandoAlgumUserExiste()
        {
            var users = new List<UserGetAllDTO>
            {
                new UserGetAllDTO
                {
                    Id = 1,
                    Name = "Lucas",
                    Email = "l@example.com",
                    PhoneNumber = "(22) 1234567890"
                },
                new UserGetAllDTO
                {
                    Id = 2,
                    Name = "Lucas Bonafé",
                    Email = "bonas@example.com",
                    PhoneNumber = "(22) 02987654321"
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
        public async Task FindById_RetornaOkEUser_QuandoOUsuarioBuscadoExiste()
        {
            int userId = 1;
            var user = new UserResponseDTO
            {
                Id = userId,
                Name = "Lucas Bonafé",
                PhoneNumber = "(22) 1234567890",
                Email = "bonas@example.com",
                AccountCreationDate = DateTime.UtcNow
            };
            _userServiceMock.Setup(service => service.GetUserByIdAsync(userId))
                .ReturnsAsync(user);

            var result = await _controller.FindById(userId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedUser = Assert.IsType<UserResponseDTO>(okResult.Value);

            Assert.Equal(user.Id, returnedUser.Id);
            Assert.Equal(user.Name, returnedUser.Name);
            Assert.Equal(user.PhoneNumber, returnedUser.PhoneNumber);
            Assert.Equal(user.Email, returnedUser.Email);
        }

        [Fact]
        public async Task Put_AtualizaUser_ReturnaNoContent()
        {
            var userUpdateDTO = new UserUpdateDTO
            {
                Name = "Lucas Bonafé",
                PhoneNumber = "(22) 9876543210",
                Email = "bonas@example.com"
            };
            int userId = 1;

            var result = await _controller.Put(userUpdateDTO, userId);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_DeletaLogicamnenteUser_ReturnaOk()
        {
            int userId = 1;
            _userServiceMock.Setup(service => service.DeleteUserAsync(userId))
                .ReturnsAsync(true);

            var result = await _controller.Delete(userId);

            Assert.IsType<OkObjectResult>(result);
        }
    }
}
