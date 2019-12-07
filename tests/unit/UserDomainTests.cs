using System;
using System.Threading.Tasks;
using Moq;
using Reboard.App.Users.Services;
using Reboard.Domain.Users;
using Reboard.Identity;
using Reboard.WebServer.Controllers;
using Xunit;

namespace Reboard.UnitTests
{
    public class UserDomainTests
    {
        [Fact]
        public async Task create_user_test()
        {
            var mockedUserService = new Mock<IUserService>();
            var hashService = new Mock<IHashService>();

            hashService.Setup(mock => mock.Encrypt("123")).Returns("#123");

            var ctrl = new UserController(mockedUserService.Object, hashService.Object);
            var result = await ctrl.Create(new CreateUserRequest { Email = "example@domain.com", Password = "123" });

            mockedUserService.Verify(mock => mock.Create("example@domain.com"), Times.Once);
            mockedUserService.Verify(mock => mock.SetPassword("example@domain.com", "#123"), Times.Once);

        }
    }
}
