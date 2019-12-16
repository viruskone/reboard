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
        public double Rating { get; set; }
    }
}