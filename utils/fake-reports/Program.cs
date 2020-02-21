using System;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Reboard.Colors;
using Reboard.Domain;
using Reboard.Domain.Reports;
using Reboard.Utils.ConsoleBase;

namespace Reboard.Utils.FakeReports
{
    public class Program : IProgram
    {
        private readonly IRepository<Report> _repository;
        private readonly Faker<Report> _faker;
        private readonly HsvContrastedColorGenerator _generator = new HsvContrastedColorGenerator(new RangeDouble(0.8, 1), new RangeDouble(1));
        private readonly Domain.Color _contrastColor = new Domain.Color { Red = 250, Green = 250, Blue = 250 };

        public Program(IRepository<Report> repository)
        {
            _repository = repository;
            _faker = new Faker<Report>("pl")
                .RuleFor(f => f.Title, (f, i) => f.Lorem.Sentence())
                .RuleFor(f => f.Description, (f, i) => f.Lorem.Sentence(10, 20))
                .RuleFor(f => f.Downloads, (f, i) => f.Random.Int(0, 10000))
                .RuleFor(f => f.Color, _ => _generator.GetContrastedColor(_contrastColor, 1.8))
                .RuleFor(f => f.Shortcut, (f, r) => _generator.GetContrastRatio(_contrastColor, r.Color).ToString("F2"))
                .RuleFor(f => f.CreateTime, (f, i) => f.Date.Between(DateTime.Now.AddYears(-5), DateTime.Now))
                .RuleFor(f => f.AverageDuration, (f, i) => TimeSpan.FromSeconds(f.Random.Double(0, 3600)));
        }

        public async Task Run(UtilMethods methods)
        {
            int reportsCount = 0;
            while (!Int32.TryParse(methods.Ask("reports count"), out reportsCount)) { }
            methods.Processing();
            await Task.WhenAll(_faker.Generate(reportsCount).Select(x => _repository.Create(x)));
        }

        static async Task Main(string[] args)
        {
            var runner = new UtilRunner<Program>(options =>
            {
                options
                    .SetTitle("Tool to generate some fake reports");
            });
            await runner.Execute();
        }
    }
}
