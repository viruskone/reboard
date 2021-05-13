using System;

namespace Reboard.Core.Application.Reports.GetReports
{
    public class GetReportsRequest
    {
        public Guid CompanyId { get; set; }
        public Guid UserId { get; set; }
    }
}