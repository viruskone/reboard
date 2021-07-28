using Reboard.Core.Domain.Base;
using Reboard.Core.Domain.Reports.OutboundServices;
using Reboard.Core.Domain.Reports.Rules;
using Reboard.Core.Domain.Shared;
using System;
using System.Collections.Generic;
using static Reboard.Core.Domain.Base.Rules.RuleValidator;

namespace Reboard.Core.Domain.Reports
{
    public class Report : Entity<ReportId>
    {
        private readonly List<CompanyId> _allowedCompanies = new List<CompanyId>();
        private readonly List<UserId> _allowedUsers = new List<UserId>();
        public IEnumerable<CompanyId> AllowedCompanies => _allowedCompanies.AsReadOnly();
        public IEnumerable<UserId> AllowedUsers => _allowedUsers.AsReadOnly();
        public TimeSpan AverageGenerationTime { get; }
        public Color Color { get; }
        public DateTime CreateTime { get; }
        public string Description { get; }
        public int DownloadTimes { get; }
        public ReportShortcut Shortcut { get; }
        public ReportTitle Title { get; }

        private Report(ReportTitle title, string description, ReportShortcut shortcut, Color color)
        {
            Title = title;
            Description = description;
            Shortcut = shortcut;
            Color = color;
            CreateTime = DateTime.Now;
            Id = Guid.NewGuid();
        }

        private Report(Guid id, ReportTitle title, string description, ReportShortcut shortcut, Color color, TimeSpan averageDuration, DateTime createTime, int downloads)
        {
            Id = id;
            Title = title;
            Description = description;
            Shortcut = shortcut;
            Color = color;
            AverageGenerationTime = averageDuration;
            CreateTime = createTime;
            DownloadTimes = downloads;
        }

        public static Report CreateNew(ReportTitle title, string description, ReportShortcut shortcut, Color color, IReportUniqueTitleChecker checker)
        {
            CheckRule(new ReportTitleMustBeUniqueRule(checker, title));
            return new Report(title, description, shortcut, color);
        }

        public static Report Make(Guid id, ReportTitle title, string description, ReportShortcut shortcut, Color color, TimeSpan averageDuration, DateTime createTime, int downloads)
            => new Report(
                id,
                title,
                description,
                shortcut,
                color,
                averageDuration,
                createTime,
                downloads);

        public void AllowAllCompanyUsers(CompanyId companyId)
        {
            if (_allowedCompanies.Contains(companyId) == false)
            {
                _allowedCompanies.Add(companyId);
            }
        }

        public void AllowUser(UserId userId)
        {
            if (_allowedUsers.Contains(userId) == false)
            {
                _allowedUsers.Add(userId);
            }
        }
    }
}