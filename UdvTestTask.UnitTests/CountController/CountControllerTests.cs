using System.Net;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using UdvTestTask.Abstractions;
using UdvTestTask.Models;
using UdvTestTask.UnitTests.FixtureAttributes;

namespace UdvTestTask.UnitTests.CountController;

public partial class CountControllerTests
{
    [Theory, AutoMoqData]
    public async Task Count_OnCall_AccountServiceInvoke([Frozen] Mock<IAuthService> authService,
        [Greedy] Controllers.CountController sut)
    {
        // act
        await sut.Count();
        
        // assert
        authService.Invocations.Count.Should().Be(1);
    }

    [Theory, AutoMoqData]
    public async Task Count_UserAuthorized_LetterCountServiceInvoke([Frozen] Mock<IAuthService> authService,
        [Frozen] Mock<ILetterCountService> counter,
        [Greedy] Controllers.CountController sut)
    {
        // arrange
        MakeAccountService(authService);
        
        // act
        await sut.Count();

        // assert
        counter.Invocations.Count.Should().Be(1);
    }

    [Theory, AutoMoqData]
    public async Task Count_UserAuthorized_PageServiceInvoke([Frozen] Mock<IAuthService> authService,
        [Frozen] Mock<IPageService> pageService, [Greedy] Controllers.CountController sut)
    {
        // arrange
        MakeAccountService(authService);

        // act
        await sut.Count();

        // assert
        pageService.Invocations.Count.Should().Be(1);
    }

    [Theory, AutoMoqData]
    public async Task Count_UserUnauthorized_ReturnsStatusCode401([Frozen] Mock<IAuthService> accountService,
        [Greedy] Controllers.CountController sut)
    {
        // arrange
        accountService.Setup(service => service.IsAuthorized()).Returns(false);
        
        // act
        var response = await sut.Count();
        var result = (response.Result as StatusCodeResult)!.StatusCode;
        
        // assert
        result.Should().Be((int)HttpStatusCode.Unauthorized);
    }

    [Theory, AutoMoqData]
    public async Task Count_AllGood_ReturnsLettersCount([Frozen] Mock<IAuthService> authService,
        [Frozen] Mock<ILetterCountService> counter, [Greedy] Controllers.CountController sut)
    {
        // arrange
        MakeAccountService(authService);

        var expectedDict = new Dictionary<char, int>()
        {
            ['a'] = 1,
            ['b'] = 2,
            ['c'] = 3
        };
        
        counter.Setup(service => service.Count(It.IsAny<PostModel[]>())).Returns(expectedDict);
        
        // act
        var response = await sut.Count();
        var result = ((response.Result as OkObjectResult)!.Value as Dictionary<char, int>)!.SequenceEqual(expectedDict);
        
        // assert
        result.Should().BeTrue();
    }


    [Theory, AutoMoqData]
    public async Task Count_UserAuthorized_LoggerInvoke([Frozen] Mock<IAuthService> authService,
        [Frozen] Mock<ILogger<Controllers.CountController>> logger, [Greedy] Controllers.CountController sut)
    {
        // arrange
        MakeAccountService(authService);
        
        // act
        await sut.Count();
        
        // assert
        logger.Invocations.Count.Should().Be(2);
    }

    [Theory, AutoMoqData]
    public async Task Count_UserAuthorized_SaveResultInDatabase([Frozen] Mock<IAuthService> authService,
        [Frozen] Mock<ILetterCountService> counter,
        [Frozen] Mock<IRepository<LettersCount>> repository, [Greedy] Controllers.CountController sut)
    {
        // arrange
        MakeAccountService(authService);

        counter.Setup(service => service.Count(It.IsAny<PostModel[]>())).Returns(new Dictionary<char, int>()
        {
            ['a'] = 1,
            ['b'] = 2
        });
        
        repository.Setup(rep => rep.AddAsync(new LettersCount()
        {
            LettersData = "{\"a\":1,\"b\":2}"
        })).Verifiable();

        // act
        await sut.Count();

        // assert
        repository.Verify();
    }
}