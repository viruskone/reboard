using System.Threading.Tasks;

namespace Reboard.Core.Domain.Reports.OutboundServices
{
    public interface IReportUniqueTitleChecker
    {
        Task<bool> IsUnique(ReportTitle title);
    }
}