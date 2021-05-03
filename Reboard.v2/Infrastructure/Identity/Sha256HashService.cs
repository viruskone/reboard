using Reboard.Core.Application.Identity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Reboard.Infrastructure.Identity
{
    public class Sha256HashService : IHashService
    {
        public string Encrypt(string content)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(content));
                return string.Concat(bytes.Select(b => b.ToString("x2")));
            }
        }
    }
}