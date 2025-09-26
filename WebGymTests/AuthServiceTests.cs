using Moq;
using Web_Application.DTOs;
using Web_Application.Services;
using Web_Domain.Entities;
using Web_Domain.Repository;

namespace WebGymTests;

public class AuthServiceTests
{
    [Fact]
    public async void LoginEmployee_ShouldReturnUser_WhenCredentialsAreValid()
    {
        //// Arrange
        //var userDto = new UserDto.UserRequest
        //(
        //    "MatiasLeo9",
        //    "Matias123"
        //);
        //var expected = new UserDto.UserResponseAuthenticated
        //(
        //    "MatiasLeo9",
        //    "Matias Leonel",
        //    "Auchana",
        //    "matias@gmail.com",
        //    1,
        //    "Administrator",
        //    "token"
        //);
        //var mockRepo = new Mock<IRepository>();
        //mockRepo.Setup(r => r.ObtenerElPrimero<User>(u => u.UserName == "MatiasLeo9"))
        //        .ReturnsAsync(new User
        //        {
        //            Id = 1,
        //            UserName = "MatiasLeo9",
        //        } );

        //            var mockTokenService = new Mock<ITokenService>();
        //mockTokenService.Setup(t => t.GenerateToken(user))
        //                .Returns("token");

        //var authService = new AuthService(mockRepo.Object, mockTokenService.Object);
        ////Act
        //var result = await authService.LoginEmployee(userDto);
        ////Assert
        //Assert.Equal(expected.userName, result.userName);
        //Assert.Equal(expected.name, result.name);
        //Assert.Equal(expected.lastName, result.lastName);
        //Assert.Equal(expected.gmail, result.gmail);
        //Assert.Equal(expected.file, result.file);
        //Assert.Equal(expected.typeEmployee, result.typeEmployee);

    }
}
