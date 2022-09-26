using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PabloTdd.Controllers;
using PabloTdd.Models;
using PabloTddUnitTests.Fixtures;

namespace PabloTddUnitTests
{
    public class TestUsersController
    {
        //We can start thiking whats the behavior of the system that we are trying to build
        [Fact]
        public async Task Get_OnSuccess_ReturnsStatusCode200()
        {
            //Arrange
            var mockUserService = new Mock<IUsersService>();
            mockUserService
                .Setup(service => service.GetAllUsers())
                .ReturnsAsync(new List<User>() {
                    new()
                    {
                        Id = 1,
                        Name="Pablin",
                        Address =new Address()
                        {
                            City="Tucuman",
                            Region = "noa",
                            PostalCode = "123"
                        },
                        Email = "pablo@gmail.com"
                    }

                });

            var sut = new UsersController(mockUserService.Object);

            //Act
            var result = (OkObjectResult)await sut.Get();

            //Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Get_OnSuccess_InvokesUsersServiceExactlyOnce()
        {
            //Arrange
            var mockUserService = new Mock<IUsersService>();

            mockUserService
                .Setup(service => service.GetAllUsers())
                .ReturnsAsync(new List<User>());

            var sut = new UsersController(mockUserService.Object);

            //Act
            var result = await sut.Get();

            //Assert
            mockUserService.Verify(service => service.GetAllUsers(), Times.Once());
        }

        [Fact]
        public async Task Get_OnSuccess_ReturnsListOfUsers()
        {
            //Arrange
            var mockUserService = new Mock<IUsersService>();

            mockUserService
                .Setup(service => service.GetAllUsers())
                .ReturnsAsync(UsersFixture.GetTestUsers()) ;

            var sut = new UsersController(mockUserService.Object);

            //Act
            var result = await sut.Get();

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;
            objectResult.Value.Should().BeOfType<List<User>>();
        }

        [Fact]
        public async Task Get_OnNoUsersFound_Returns404()
        {
            //Arrange
            var mockUserService = new Mock<IUsersService>();

            mockUserService
                .Setup(service => service.GetAllUsers())
                .ReturnsAsync(new List<User>());

            var sut = new UsersController(mockUserService.Object);

            //Act
            var result = await sut.Get();

            //Assert
            result.Should().BeOfType<NotFoundResult>();
            var objectResult = (NotFoundResult)result;
            objectResult.StatusCode.Should().Be(404);
        }

        [Theory] //Allows to put parameters
        [InlineData("thing1", 1)] //For each inlineData row, the test will run inyecting diferent params
        [InlineData("thing2", 2)] 

        public void Test2(string thing, int number)
        {

        }
    }
}