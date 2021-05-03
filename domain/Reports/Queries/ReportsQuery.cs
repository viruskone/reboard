using Reboard.CQRS;
using Reboard.Domain.Users;
using System.Collections.Generic;

namespace Reboard.Domain.Reports.Queries
{
    public class ReportsQuery : IQuery<IEnumerable<Report>>
    {
        public User User { get; set; }
    }
}