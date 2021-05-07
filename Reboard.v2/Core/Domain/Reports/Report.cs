using Reboard.Core.Domain.Base;
using Reboard.Core.Domain.Reports.OutboundServices;
using Reboard.Core.Domain.Reports.Rules;
using System;
using static Reboard.Core.Domain.Base.Rules.BusinessRuleValidator;

namespace Reboard.Core.Domain.Reports
{
    public class Report : Entity
    {
        public TimeSpan AverageGenerationTime { get; }
        public Color Color { get; }
        public DateTime CreateTime { get; }
        public string Description { get; }
        public int DownloadTimes { get; }
        public string Shortcut { get; }
        public ReportTitle Title { get; }

        private Report(ReportTitle title, string description, string shortcut, Color color)
        {
            Title = title;
            Description = description;
            Shortcut = shortcut;
            Color = color;
            CreateTime = DateTime.Now;
        }

        public static Report CreateNew(ReportTitle title, string description, string shortcut, Color color, IReportUniqueTitleChecker checker)
        {
            CheckRule(new ReportTitleMustBeUniqueRule(checker, title));
            return new Report(title, description, shortcut, color);
        }
    }
}