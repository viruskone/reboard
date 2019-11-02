using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reboard.CQRS;
using Reboard.Domain;
using Reboard.Domain.Reports;
using Reboard.Domain.Reports.Queries;

namespace Reboard.App.Reports.QueryHandlers
{
    public class ReportsQueryHandler : IQueryHandler<ReportsQuery, IEnumerable<Report>>
    {
        private readonly IRepository<Report> _repository;

        public ReportsQueryHandler(IRepository<Report> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Report>> HandleAsync(ReportsQuery query)
            => await _repository.GetAll();
            
    }
}
