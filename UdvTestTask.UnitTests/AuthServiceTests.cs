using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using UdvTestTask.Models;
using UdvTestTask.Services;
using UdvTestTask.UnitTests.FixtureAttributes;
using VkNet.Abstractions;
using VkNet.Model;

namespace UdvTestTask.UnitTests;

public class AuthServiceTests
{
    [Theory, AutoMoqData]
    public void IsAuthorized_ByDefault_IsFalse(AuthService sut)
    {
        // act
        var result = sut.IsAuthorized();

        // assert
        result.Should().BeFalse();
    }

    [Theory, AutoMoqData]
    public async Task IsAuthorized_SuccessAuthorization_IsTrue([Frozen] Mock<IVkApi> api, AuthService sut)
    {
        // arrange
        api.SetupGet(vkApi => vkApi.Token).Returns("token");
        
        // act
        await sut.TryAuthAsync(new UserModel());
        
        var result = sut.IsAuthorized();
        
        // assert
        result.Should().BeTrue();
    }


    [Theory, AutoMoqData]
    public async Task TryAuthAsync_ApiThrows_ReturnsResultNotOk([Frozen] Mock<IVkApi> api, AuthService sut)
    {
        // arrange
        api.Setup(vkApi => vkApi.AuthorizeAsync(It.IsAny<IApiAuthParams>())).Throws(new Exception());
        
        // arrange
        var result = await sut.TryAuthAsync(new UserModel());
        
        // assert
        result.Ok.Should().BeFalse();
    }
}