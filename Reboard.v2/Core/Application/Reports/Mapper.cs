using Reboard.Core.Application.Reports.GetReports;
using Reboard.Core.Domain.Reports;
using Reboard.Core.Domain.Shared;

namespace Reboard.Core.Application.Reports
{
    internal static class Mapper
    {
        internal static ReportDto ToDto(this Report report)
            => new ReportDto
            {
                Id = report.Id,
                Title = report.Title,
                Description = report.Description,
                Shortcut = report.Shortcut,
                Color = report.Color.ToDto(),
                CreateTime = report.CreateTime,
                AverageGenerationTime = report.AverageGenerationTime,
                DownloadTimes = report.DownloadTimes
            };

        internal static ColorDto ToDto(this Color color)
            => new ColorDto(color.Red, color.Green, color.Blue);
    }
}