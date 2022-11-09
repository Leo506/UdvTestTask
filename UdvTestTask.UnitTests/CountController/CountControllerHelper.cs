using Moq;
using UdvTestTask.Abstractions;

namespace UdvTestTask.UnitTests.CountController;

public static class CountControllerHelper
{
    public static void MakeAccountService(Mock<IAccountService> account, bool isAuthorized = true)
    {
        account.Setup(service => service.IsAuthorized()).Returns(isAuthorized);
    }
}