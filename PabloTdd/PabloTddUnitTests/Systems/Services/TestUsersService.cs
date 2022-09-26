using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using PabloTdd.Config;
using PabloTdd.Models;
using PabloTddUnitTests.Fixtures;
using PabloTddUnitTests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PabloTddUnitTests.Systems.Services
{
    public class TestUsersService
    {
        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesHttpGetRequest()
        {
            //Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);
            var endpoint = "http://example.com";
            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endpoint
            });
            var sut = new UsersService(httpClient,config);


            //Act
            await sut.GetAllUsers();


            //Assert
            handlerMock
                .Protected()
                .Verify("SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method== HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
                );

            //Verify HTTP requestis made!
        }

        [Fact]
        public async Task GetAllUsers_WhenHits404_ReturnsEmptyListOfUsers()
        {
            //Arrange
            var handlerMock = MockHttpMessageHandler<User>.SetupReturn404();
            var httpClient = new HttpClient(handlerMock.Object);
            var endpoint = "http://example.com";
            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endpoint
            });
            var sut = new UsersService(httpClient, config);

            //Act
            var result = await sut.GetAllUsers();


            //Assert
            result.Count.Should().Be(0);

        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnsListOfUsersOfExpectedSize()
        {
            //Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);
            var endpoint = "http://example.com";
            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endpoint
            });
            var sut = new UsersService(httpClient, config);

            //Act
            var result = await sut.GetAllUsers();


            //Assert
            result.Count.Should().Be(expectedResponse.Count);

        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesConfiguredExternalUrl()
        {
            //Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var endpoint = "http://example.com";
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse, endpoint);
            var httpClient = new HttpClient(handlerMock.Object);

            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endpoint
            });


            var sut = new UsersService(httpClient, config);

            //Act
            var result = await sut.GetAllUsers();
            var uri = new Uri(endpoint);

            //Assert
            handlerMock
                .Protected()
                .Verify("SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(
                    req => req.Method == HttpMethod.Get 
                    && req.RequestUri == uri),
                ItExpr.IsAny<CancellationToken>()
                );

        }
    }
}
