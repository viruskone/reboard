using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Reboard.WebServer;

namespace Reboard.Utils.ConsoleBase
{
    public class UtilRunner<TProgram>
        where TProgram : class, IProgram
    {

        private readonly TProgram _program;
        private readonly ProgramOptions _options;

        public UtilRunner(Action<ProgramOptions> optionsConfigure)
        {
            _options = new ProgramOptions();
            optionsConfigure(_options);
            var services = new ServiceCollection();
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var startup = new Startup(configuration);
            startup.ConfigureServices(services);

            services.AddTransient<TProgram>();
            services.AddLogging(configure =>
            {
                configure.AddConsole();
            });

            var servicesProvider = services.BuildServiceProvider();
            _program = servicesProvider.GetService(typeof(TProgram)) as TProgram;
        }

        public async Task Execute()
        {
            var utilMethods = new UtilMethods();
            Console.Clear();
            Console.WriteLine(_options.DisplayTitle);
            Console.WriteLine();
            await _program.Run(utilMethods);
            utilMethods.Completed();
            Console.WriteLine("Type enter to terminate");
            Console.ReadLine();
        }

    }
}
