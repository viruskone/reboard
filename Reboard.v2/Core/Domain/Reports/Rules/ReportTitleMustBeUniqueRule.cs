using Reboard.Core.Domain.Base.Rules;
using Reboard.Core.Domain.Reports.OutboundServices;

namespace Reboard.Core.Domain.Reports.Rules
{
    public class ReportTitleMustBeUniqueRule : IBusinessRule
    {
        private readonly ReportTitle _title;
        private IReportUniqueTitleChecker _checker;
        public string Message => $"{_title} already exist";

        public ReportTitleMustBeUniqueRule(IReportUniqueTitleChecker checker, ReportTitle reportTitle)
        {
            _checker = checker;
            _title = reportTitle;
        }

        public bool IsBroken()
            => _checker.IsUnique(_title).Result == false;
    }
}