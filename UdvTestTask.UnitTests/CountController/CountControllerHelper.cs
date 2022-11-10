using Moq;
using UdvTestTask.Abstractions;

namespace UdvTestTask.UnitTests.CountController;

public partial class CountControllerTests
{
    private static void MakeAccountService(Mock<IAuthService> account, bool isAuthorized = true)
    {
        account.Setup(service => service.IsAuthorized()).Returns(isAuthorized);
    }
    
}