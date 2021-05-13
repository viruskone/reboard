using Reboard.Core.Domain.Reports;
using System;

namespace Reboard.Core.Application.Reports.GetReports
{
    public class ReportDto
    {
        public TimeSpan AverageGenerationTime { get; set; }
        public ColorDto Color { get; set; }
        public DateTime CreateTime { get; set; }
        public string Description { get; set; }
        public int DownloadTimes { get; set; }
        public Guid Id { get; set; }
        public ReportShortcut Shortcut { get; set; }
        public ReportTitle Title { get; set; }
    }
}