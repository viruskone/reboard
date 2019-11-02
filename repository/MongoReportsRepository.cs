using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Reboard.Domain;
using Reboard.Domain.Reports;

namespace Reboard.Repository
{
    public class MongoReportsRepository : IRepository<Report>
    {

        private readonly IMongoCollection<ReportMongoDto> _collection;

        public MongoReportsRepository(MongoConnection connection)
        {
            
            var client = new MongoClient(connection.Value);
            var database = client.GetDatabase("reboard");
            _collection = database.GetCollection<ReportMongoDto>("reports");
        }

        public async Task<Report> Create(Report newEntity)
        {
            var dto = newEntity.ToDto().AssingNewId();
            await _collection.InsertOneAsync(dto);
            return await Get(dto.Id.ToString());
        }

        public async Task Delete(string id)
            => await _collection.DeleteOneAsync(GetFilterById(id));

        public async Task<Report> Get(string id)
        {
            var result = await _collection.FindAsync(GetFilterById(id));
            return (await result.FirstOrDefaultAsync()).FromDto();
        }

        public async Task<IEnumerable<Report>> GetAll()
            => (await _collection.AsQueryable().ToListAsync()).Select(dto => dto.FromDto());

        public async Task<Report> Update(Report newEntity)
        {
            await _collection.ReplaceOneAsync(GetFilterById(newEntity.Id), newEntity.ToDto());
            return await Get(newEntity.Id);
        }

        private FilterDefinition<ReportMongoDto> GetFilterById(string id)
            => new FilterDefinitionBuilder<ReportMongoDto>().Eq(dto => dto.Id, new ObjectId(id));

    }
}
