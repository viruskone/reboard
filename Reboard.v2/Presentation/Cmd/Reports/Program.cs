using Bogus;
using MediatR;
using Reboard.Core.Application.Colors;
using Reboard.Core.Application.Reports;
using Reboard.Core.Application.Reports.CreateReport;
using Reboard.Core.Domain.Shared;
using Reboard.Core.Domain.Users;
using Reboard.Core.Domain.Users.OutboundServices;
using Reboard.Presentation.Cmd.CmdBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reports
{
    public class Program : IProgram
    {
        private readonly Rgb _contrastColor = new Rgb(250, 250, 250);
        private readonly Faker _faker;
        private readonly HsvContrastedColorGenerator _generator = new HsvContrastedColorGenerator(new RangeDouble(0.8, 1), new RangeDouble(1));
        private readonly IMediator _mediator;
        private readonly IUserRepository _userRepository;

        public Program(IMediator mediator, IUserRepository userRepository)
        {
            _mediator = mediator;
            _userRepository = userRepository;
            _faker = new Faker("pl");
        }

        public async Task Run(CommandLineMethods methods)
        {
            int reportsCount = 0;
            while (!int.TryParse(methods.Ask("reports count"), out reportsCount)) { }
            var forUser = methods.Ask("For users (leave empty if you want set that by company)");
            var forCompany = methods.Ask("For company");
            methods.Processing();

            await Task.WhenAll(Enumerable.Range(0, reportsCount).Select(async i => await CreateReport(forUser, forCompany)));
        }

        private static async Task Main(string[] args)
        {
            var runner = new CommandLineRunner<Program>(options =>
            {
                options
                    .SetTitle("Tool to generate some fake reports");
            });
            await runner.Execute();
        }

        private async Task CreateReport(string forUser, string forCompany)
        {
            var color = _generator.GetContrastedColor(_contrastColor, 1.8);
            var users = await GetUserIds(forUser);
            var companies = await GetCompanyIds(forCompany);
            var request = new CreateReportCommand(
                _faker.Lorem.Sentence(),
                _faker.Lorem.Sentence(10, 20),
                _generator.GetContrastRatio(_contrastColor, color).ToString("F2"),
                new ColorDto(color.Red, color.Green, color.Blue),
                users.Select(uid => uid.Value).ToArray(),
                companies.Select(uid => uid.Value).ToArray());
            await _mediator.Send(request);
        }

        private async Task<IEnumerable<CompanyId>> GetCompanyIds(string forCompanies)
        {
            if (string.IsNullOrWhiteSpace(forCompanies)) return new CompanyId[0];
            var companies = forCompanies.Split(",").Select(s => s.Trim());
            var tasks = companies.Select(companyName =>
                 _userRepository.GetCompany((CompanyName)companyName));
            await Task.WhenAll(tasks);
            return tasks.Select(t => t.Result.Id).ToList();
        }

        private async Task<IEnumerable<UserId>> GetUserIds(string forUser)
        {
            if (string.IsNullOrWhiteSpace(forUser)) return new UserId[0];
            var usersLogin = forUser.Split(",").Select(s => s.Trim());
            var tasks = usersLogin.Select(async login =>
                await _userRepository.Get((Login)login));
            await Task.WhenAll(tasks);
            return tasks.Select(t => t.Result.Id);
        }
    }
}