using System.Threading.Tasks;

namespace Reboard.Core.Domain.Users.OutboundServices
{
    public interface IUserUniqueLoginChecker
    {
        Task<bool> IsUnique(string value);
    }
}