using Reboard.CQRS;
using System.Collections.Generic;

namespace Reboard.Domain.Reports.Queries
{
    public class ReportsQuery : IQuery<IEnumerable<Report>>
    {
        public string ForUser { get; set; }
        public string ForCompany { get; set; }
    }
}