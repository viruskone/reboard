﻿using MongoDB.Bson;
using System;

namespace Reboard.Repository.Auth.Mongo
{
    internal static class AuthCollectionExtensions
    {
        internal static FailedAuthDto ToFailedDto(this Domain.Auth.Auth auth)
            => new FailedAuthDto
            {
                Id = new ObjectId(auth.RequestId),
                User = auth.User,
                CreateTime = auth.Time
            };

        internal static Domain.Auth.Auth FromDto(this FailedAuthDto dto)
            => new Domain.Auth.Auth
            {
                Status = Domain.Auth.AuthStatus.Failed,
                Time = DateTime.SpecifyKind(dto.CreateTime, DateTimeKind.Utc),
                RequestId = dto.Id.ToString(),
                User = dto.User
            };

        internal static SuccessAuthDto ToSuccessDto(this Domain.Auth.Auth auth)
            => new SuccessAuthDto
            {
                Id = new ObjectId(auth.RequestId),
                User = auth.User,
                CreateTime = auth.Time,
                Token = auth.Token
            };

        internal static Domain.Auth.Auth FromDto(this SuccessAuthDto dto)
            => new Domain.Auth.Auth
            {
                Status = Domain.Auth.AuthStatus.Success,
                Time = DateTime.SpecifyKind(dto.CreateTime, DateTimeKind.Utc),
                RequestId = dto.Id.ToString(),
                User = dto.User,
                Token = dto.Token
            };

    }
}