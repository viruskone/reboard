using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reboard.Core.Domain.Reports.OutboundServices
{
    public interface IReportRepository
    {
        Task<IEnumerable<Report>> GetByCompany(Guid companyId);

        Task<Report> GetByTitle(ReportTitle title);

        Task<IEnumerable<Report>> GetByUser(Guid userId);

        Task Save(Report report);
    }
}