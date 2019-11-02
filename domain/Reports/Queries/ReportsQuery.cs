using System;
using System.Collections.Generic;
using Reboard.CQRS;

namespace Reboard.Domain.Reports.Queries
{
    public class ReportsQuery : IQuery<IEnumerable<Report>>
    {
    }
}
