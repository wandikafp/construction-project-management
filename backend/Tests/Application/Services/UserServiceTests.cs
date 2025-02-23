using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Services;
using Application.Services;
using Domain.Entities;
using Domain.Repositories;
using Moq;

namespace Tests.Application.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly IUserService _userService;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userService = new UserService(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task ValidateUserCredentials_ValidCredentials_ReturnsUser()
        {
            // Arrange
            var email = "test@example.com";
            var password = "password";
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            var user = new User { Email = email, PasswordHash = passwordHash };
            _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(email)).ReturnsAsync(user);

            // Act
            var result = await _userService.ValidateUserCredentials(email, password);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(email, result.Email);
        }

        [Fact]
        public async Task ValidateUserCredentials_InvalidCredentials_ReturnsNull()
        {
            // Arrange
            var email = "test@example.com";
            var password = "password";
            var user = new User { Email = email, PasswordHash = BCrypt.Net.BCrypt.HashPassword("wrongpassword") };
            _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(email)).ReturnsAsync(user);

            // Act
            var result = await _userService.ValidateUserCredentials(email, password);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void HashPassword_ReturnsHashedPassword()
        {
            // Arrange
            var password = "password";

            // Act
            var result = _userService.HashPassword(password);

            // Assert
            Assert.True(BCrypt.Net.BCrypt.Verify(password, result));
        }
    }
}
