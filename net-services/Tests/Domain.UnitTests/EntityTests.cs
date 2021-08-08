using FluentAssertions;
using Reboard.Core.Domain.Reports;
using Reboard.Core.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Reboard.Tests.Domain.UnitTests
{
    public class EntityTests
    {
        [Fact]
        public void two_entities_with_same_id_should_equal()
        {
            var id = Guid.NewGuid();

            var report1 = Report.Make(
                id,
                (ReportTitle)"abc",
                "abecadło",
                (ReportShortcut)"a",
                Color.Make(0, 0, 0),
                TimeSpan.Zero,
                DateTime.Now,
                0);
            var report2 = Report.Make(
                report1.Id,
                report1.Title,
                report1.Description,
                report1.Shortcut,
                report1.Color,
                report1.AverageGenerationTime,
                report1.CreateTime,
                report1.DownloadTimes);

            new[] { report1, report2 }.Distinct().Should().HaveCount(1);
        }
    }
}