using System.Threading.Tasks;

namespace Reboard.Core.Domain.Users.OutboundServices
{
    public interface ICompanyUniqueNameChecker
    {
        Task<bool> IsUnique(string value);
    }
}