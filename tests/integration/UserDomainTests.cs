using System;
using System.Threading.Tasks;
using AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Testing;
using Reboard.Domain.Users;
using Reboard.WebServer;
using Xunit;

namespace Reboard.IntegrationTests
{
    public class UserDomainTests : IClassFixture<WebApplicationFactory<Reboard.WebServer.Program>>
    {

        private readonly WebApplicationFactory<Reboard.WebServer.Program> _factory;

        public UserDomainTests(WebApplicationFactory<Reboard.WebServer.Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Test1()
        {
            var client = _factory.CreateClient();
            var response = await client.PostAsJsonAsync<CreateUserRequest>("api/user", new CreateUserRequest { Email = "example@domain.com", Password = "123" });
            response.EnsureSuccessStatusCode();
        }
    }
}
