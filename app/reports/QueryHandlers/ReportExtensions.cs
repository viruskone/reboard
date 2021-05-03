using Reboard.Domain.Reports;
using Reboard.Domain.Users;
using System.Linq;

namespace Reboard.App.Reports.QueryHandlers
{
    internal static class ReportExtensions
    {
        internal static bool ReportAllowedForUser(this Report report, User user) =>
            report.AllowedUsers.Contains("*")
            || report.AllowedUsers.Contains(user.Login)
            || report.AllowedCompanies.Contains(user.Company);
    }


}