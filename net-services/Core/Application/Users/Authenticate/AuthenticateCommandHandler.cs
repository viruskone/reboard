﻿using MediatR;
using Reboard.Core.Application.Identity;
using Reboard.Core.Domain.Users;
using Reboard.Core.Domain.Users.OutboundServices;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Reboard.Core.Application.Users.Authenticate
{
    public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, AuthenticateResponse>
    {
        private readonly IHashService _hashService;
        private readonly ITokenFactory _tokenFactory;
        private readonly IUserRepository _userRepository;

        public AuthenticateCommandHandler(IUserRepository userRepository, ITokenFactory tokenFactory, IHashService hashService)
        {
            _userRepository = userRepository;
            _tokenFactory = tokenFactory;
            _hashService = hashService;
        }

        public async Task<AuthenticateResponse> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            Password password = Password.MakeNew(request.Password, _hashService);

            var user = await _userRepository.Get((Login)request.Login);
            if (user == null) return Failed();

            return user.Password == password ?
                 Success(user) :
                 Failed();
        }

        private AuthenticateResponse Failed()
            => new AuthenticateResponse { Status = AuthenticateStatus.Failed };

        private AuthenticateResponse Success(User user)
        {
            var token = _tokenFactory.Create();
            token.SetName(user.Login);
            token.AddClaim("user", JsonSerializer.Serialize(new UserClaims { UserId = user.Id, CompanyId = user.Company.Id }));
            token.SetExpiration(TimeSpan.FromDays(7));
            return new AuthenticateResponse
            {
                Status = AuthenticateStatus.Success,
                Token = token.Generate()
            };
        }
    }
}