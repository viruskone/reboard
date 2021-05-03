using Reboard.CQRS;
using Reboard.Domain.Users;

namespace Reboard.Domain.Reports.Queries
{
    public class ReportQuery : IQuery<Report>
    {
        public string Id { get; set; }
        public User User { get; set; }
    }
}