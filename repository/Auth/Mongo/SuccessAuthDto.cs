using MongoDB.Bson;
using Reboard.Repository.Mongo;

namespace Reboard.Repository.Auth.Mongo
{
    public class SuccessAuthDto : AuthDto
    {
        public string Token { get; set; }
    }

}