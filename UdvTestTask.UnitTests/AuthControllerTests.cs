using System.Net;
using AutoFixture.Xunit2;
using Calabonga.OperationResults;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UdvTestTask.Abstractions;
using UdvTestTask.Controllers;
using UdvTestTask.Models;
using UdvTestTask.UnitTests.FixtureAttributes;

namespace UdvTestTask.UnitTests;

public class AuthControllerTests
{
    [Theory, AutoMoqData]
    public async Task Auth_AllGood_ReturnsStatusCode200([Greedy] AuthController sut)
    {
        // act
        var response = await sut.Auth(new UserModel()
        {
            Login = "login",
            Password = "password"
        });


        var result = (response as StatusCodeResult)!.StatusCode;
        
        // assert
        result.Should().Be((int)HttpStatusCode.OK);
    }

    [Theory, AutoMoqData]
    public async Task Auth_AllGood_AuthServiceInvoke([Frozen] Mock<IAuthService> authService, [Greedy] AuthController sut)
    {
        // act
        await sut.Auth(new UserModel());

        // assert
        authService.Invocations.Count.Should().Be(1);
    }

    [Theory, AutoMoqData]
    public async Task Auth_AuthServiceReturnsResultNotOk_ReturnsStatusCode401([Frozen] Mock<IAuthService> authService,
        [Greedy] AuthController sut)
    {
        // arrange
        authService.Setup(service => service.TryAuthAsync(It.IsAny<UserModel>())).ReturnsAsync(
            new OperationResult<bool>()
            {
                Exception = new Exception()
            });
        
        // act
        var response = await sut.Auth(new UserModel());
        var result = (response as StatusCodeResult)!.StatusCode;
        
        // assert
        result.Should().Be((int)HttpStatusCode.Unauthorized);
    }

    [Theory, AutoMoqData]
    public async Task Auth_UserModelHasErrors_ReturnsStatusCode400([Greedy] AuthController sut)
    {
        // arrange
        sut.ViewData.ModelState.AddModelError("error", "error");

        // act
        var response = await sut.Auth(new UserModel());
        var result = (response as StatusCodeResult)!.StatusCode;

        // assert
        result.Should().Be((int)HttpStatusCode.BadRequest);
    }
}