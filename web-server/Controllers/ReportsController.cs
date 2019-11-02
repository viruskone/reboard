using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reboard.Domain;
using Reboard.Domain.Reports;

namespace Reboard.WebServer.Controllers
{
    [Route("api/reports")]
    [Authorize]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IRepository<Report> _repository;
        private readonly Faker<Report> _faker;

        public ReportsController(IRepository<Report> repository)
        {
            _repository = repository;
            _faker = new Faker<Report>("pl")
                .RuleFor(f => f.Title, (f, i) => f.Lorem.Sentence())
                .RuleFor(f => f.Description, (f, i) => f.Lorem.Sentence(10, 20))
                .RuleFor(f => f.Downloads, (f, i) => f.Random.Int(0, 10000))
                .RuleFor(f => f.Rating, (f, i) => f.Random.Double(0, 5))
                .RuleFor(f => f.CreateTime, (f, i) => f.Date.Between(DateTime.Now.AddYears(-5), DateTime.Now))
                .RuleFor(f => f.AverageDuration, (f, i) => TimeSpan.FromSeconds(f.Random.Double(0, 3600)));

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Report>>> Get()
        {
            var result = await _repository.GetAll();
            if (!result.Any())
            {
                await Task.WhenAll(_faker.Generate(new Random().Next(30)).Select(x => _repository.Create(x)));
            }
            return Ok(await _repository.GetAll());
        }

    }
}