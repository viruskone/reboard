using MediatR;
using System;
using System.Collections.Generic;

namespace Reboard.Core.Application.Reports.GetReports
{
    public class GetReportsQuery : IRequest<IEnumerable<ReportDto>>
    {
        public Guid CompanyId { get; }
        public Guid UserId { get; }

        public GetReportsQuery(Guid userId, Guid companyId)
        {
            UserId = userId;
            CompanyId = companyId;
        }
    }
}