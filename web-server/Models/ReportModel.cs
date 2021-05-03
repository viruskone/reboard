using Reboard.Domain;
using Reboard.Domain.Reports;
using System;

namespace Reboard.WebServer.Models
{
    public class ReportModel
    {
        public string Id { get; internal set; }
        public string Title { get; internal set; }
        public string Description { get; internal set; }
        public TimeSpan AverageDuration { get; internal set; }
        public DateTime CreateTime { get; internal set; }
        public int Downloads { get; internal set; }
        public string Shortcut { get; internal set; }
        public ColorModel Color { get; internal set; }
    }

    internal static class ReportModelAdapter
    {
        internal static ReportModel FromDomain(this Report report) =>
            new ReportModel
            {
                Id = report.Id,
                Title = report.Title,
                Description = report.Description,
                AverageDuration = report.AverageDuration,
                CreateTime = report.CreateTime,
                Downloads = report.Downloads,
                Shortcut = report.Shortcut,
                Color = report.Color.FromDomain()
            };
    }
    
}
