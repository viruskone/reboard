using System;

namespace Reboard.Core.Application.Reports.CreateReport
{
    public class CreateReportRequest
    {
        public Guid[] AllowedCompanies { get; }
        public Guid[] AllowedUsers { get; }
        public ColorDto Color { get; set; }
        public string Description { get; set; }
        public string Shortcut { get; set; }
        public string Title { get; set; }
    }
}