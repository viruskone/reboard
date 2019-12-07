using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reboard.App.Users.Services;
using Reboard.Domain.Users;
using Reboard.Identity;

namespace Reboard.WebServer.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IHashService _hashing;

        public UserController(IUserService service, IHashService hashService)
        {
            _service = service;
            _hashing = hashService;
        }

        [HttpGet]
        public async Task<OkObjectResult> GetUser(string email)
            => Ok(await _service.Get(email));

        [HttpPost]
        public async Task<CreatedAtActionResult> Create(CreateUserRequest request)
        {
            await CreateUser(request.Email);
            await SetPassword(request.Email, request.Password);
            return CreatedAtAction(nameof(GetUser), null);
        }

        private async Task CreateUser(string email)
        {
            try
            {
                await _service.Create(email);
            }
            catch (UserException exception) when (exception.Type == UserException.ErrorType.UserAlreadyExist) { }
        }

        private async Task SetPassword(string email, string password)
            => await _service.SetPassword(email, _hashing.Encrypt(password));

    }
}
