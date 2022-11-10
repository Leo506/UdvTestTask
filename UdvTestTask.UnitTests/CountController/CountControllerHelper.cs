using Calabonga.OperationResults;
using Moq;
using UdvTestTask.Abstractions;
using UdvTestTask.Models;

namespace UdvTestTask.UnitTests.CountController;

public partial class CountControllerTests
{
    private static void MakeAccountService(Mock<IAuthService> account, bool isAuthorized = true)
    {
        account.Setup(service => service.IsAuthorized()).Returns(isAuthorized);
    }

    private static void MakePageService(Mock<IPageService> page, bool hasError = false)
    {
        page.Setup(service => service.GetLastPosts(It.IsAny<int>())).ReturnsAsync(
            new OperationResult<IList<PostModel>>()
            {
                Exception = hasError ? new Exception() : null,
                Result = hasError ? null : new List<PostModel>()
            });
    }
}