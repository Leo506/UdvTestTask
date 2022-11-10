using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using UdvTestTask.Services;
using UdvTestTask.UnitTests.FixtureAttributes;
using VkNet.Abstractions;
using VkNet.Model;
using VkNet.Model.Attachments;
using VkNet.Model.RequestParams;
using VkNet.Utils;

namespace UdvTestTask.UnitTests;

public class PageServiceTests
{
    [Theory, AutoMoqData]
    public async Task GetLastPosts_AllGood_ReturnsPostsListWith5Posts([Frozen] Mock<IVkApi> api, PageService sut)
    {
        // arrange
        api.Setup(vkApi => vkApi.Wall.GetAsync(It.IsAny<WallGetParams>(), It.IsAny<bool>())).ReturnsAsync(
            new WallGetObject()
            {
                WallPosts = new VkCollection<Post>(5, new[]
                {
                    new Post()
                    {
                        Text = "text"
                    },
                    new Post()
                    {
                        Text = "text"
                    },
                    new Post()
                    {
                        Text = "text"
                    },
                    new Post()
                    {
                        Text = "text"
                    },
                    new Post()
                    {
                        Text = "text"
                    }
                })
            });

        // act
        var result = await sut.GetLastPosts();

        // assert
        result.Result!.Count.Should().Be(5);
    }

    [Theory, AutoMoqData]
    public async Task GetLastPosts_ApiThrows_ReturnsResultNotOk([Frozen] Mock<IVkApi> api, PageService sut)
    {
        // arrange
        api.Setup(vkApi => vkApi.Wall.GetAsync(It.IsAny<WallGetParams>(), It.IsAny<bool>())).Throws(new Exception());
        
        // act
        var result = await sut.GetLastPosts();
        
        // assert
        result.Ok.Should().BeFalse();
    }
}