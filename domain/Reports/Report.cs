using System;

namespace Reboard.Domain.Reports
{
    public class Report
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }
        public int Downloads { get; set; }
        public TimeSpan AverageDuration { get; set; }
        public Color Color { get; set; }
        public string Shortcut { get; set; }
        public string[] AllowedUsers { get; set; }
        public string[] AllowedCompanies { get; set; }
    }
}