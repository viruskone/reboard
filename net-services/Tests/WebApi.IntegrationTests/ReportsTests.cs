using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Reboard.Core.Application.Reports.GetReports;
using Reboard.Core.Domain.Reports;
using Reboard.Core.Domain.Reports.OutboundServices;
using Reboard.Core.Domain.Shared;
using Reboard.Core.Domain.Users;
using Reboard.Presentation.WebApi;
using System;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Reboard.Tests.WebApi.IntegrationTests
{
    public class ReportsTests : IntegrationTestBase
    {
        private readonly IReportUniqueTitleChecker alwaysUniqueChecker;

        public ReportsTests(WebApplicationFactory<Startup> factory, ITestOutputHelper outputHelper, Action<IServiceCollection> configureServices = null)
            : base(factory, outputHelper, configureServices)
        {
            var uniqueTitleCheckerMock = new Mock<IReportUniqueTitleChecker>();
            uniqueTitleCheckerMock.Setup(mock => mock.IsUnique(It.IsAny<ReportTitle>())).ReturnsAsync(true);
            alwaysUniqueChecker = uniqueTitleCheckerMock.Object;
        }

        [Fact]
        public async Task get_reports()
        {
            const string login = nameof(get_reports);
            const string company = "INC";
            const string password = "Trudne4YOU!";
            const int reportCount = 10;

            var client = await CreateAuthenticatedClient(login, company, password);

            var createdReports = Enumerable.Range(1, reportCount).Select(salt => CreateAndSaveReport(salt)).ToList();
            var user = await UserRepository.Get(Login.Make(login));

            var response = await client.GetAsync("api/reports");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var reports = await response.Content.ReadFromJsonAsync<ReportDto[]>();
            reports.Should().HaveCount(0);

            var reportsAssignedToUser = createdReports.Take(4);
            var reportsAssignedToCompany = createdReports.Skip(2).Take(4);

            reportsAssignedToUser.ToList().ForEach(r => r.AllowUser(user.Id));
            reportsAssignedToCompany.ToList().ForEach(r => r.AllowAllCompanyUsers(user.Company.Id));
            createdReports.ToList().ForEach(r => ReportRepository.Save(r));

            response = await client.GetAsync("api/reports");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            reports = await response.Content.ReadFromJsonAsync<ReportDto[]>();
            reports.Should().HaveCount(6);
        }

        private Report CreateAndSaveReport(int randomSalt)
        {
            var report = Report.CreateNew(
                ReportTitle.Make($"Report {randomSalt}"),
                $"Report {randomSalt} details",
                ReportShortcut.Make(randomSalt.ToString()),
                Color.Make(randomSalt, randomSalt, randomSalt),
                alwaysUniqueChecker
                );
            ReportRepository.Save(report);
            return report;
        }
    }
}