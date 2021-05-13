using System.Threading.Tasks;

namespace Reboard.Core.Domain.Users.OutboundServices
{
    public interface IUserRepository
    {
        Task<User> Get(Login login);

        Task<Company> GetCompany(CompanyName name);

        Task Save(User user);

        Task Save(Company company);
    }
}