using MongoDB.Driver;
using Reboard.Core.Domain.Reports;
using Reboard.Core.Domain.Reports.OutboundServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reboard.Infrastructure.MongoDB.Reports
{
    public class ReportRepository : IReportRepository
    {
        private readonly IMongoCollection<ReportDto> _collection;

        public ReportRepository(MongoConnection connection)
        {
            _collection = connection.GetCollection<ReportDto>();
        }

        public async Task<IEnumerable<Report>> GetByCompany(Guid companyId)
        {
            var companyFilter = new FilterDefinitionBuilder<ReportDto>().AnyEq(dto => dto.AllowedCompanies, companyId);
            var result = await _collection.FindAsync(companyFilter);
            return (await result.ToListAsync()).Select(dto => dto.FromDto());
        }

        public async Task<Report> GetByTitle(ReportTitle title)
        {
            var titleFilter = new FilterDefinitionBuilder<ReportDto>().Eq(dto => dto.Title, title.Value);
            var result = await _collection.FindAsync(titleFilter);
            return (await result.FirstOrDefaultAsync()).FromDto();
        }

        public async Task<IEnumerable<Report>> GetByUser(Guid userId)
        {
            var userFilter = new FilterDefinitionBuilder<ReportDto>().AnyEq(dto => dto.AllowedUsers, userId);
            var result = await _collection.FindAsync(userFilter);
            return (await result.ToListAsync()).Select(dto => dto.FromDto());
        }

        public async Task Save(Report report)
        {
            var idFilter = new FilterDefinitionBuilder<ReportDto>().Eq(dto => dto.Id, report.Id.Value);
            await _collection.ReplaceOneAsync(idFilter, report.ToDto(), new ReplaceOptions { IsUpsert = true });
        }
    }
}