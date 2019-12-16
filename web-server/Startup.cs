using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Reboard.App.Users.Services;
using Reboard.CQRS;
using Reboard.Domain;
using Reboard.Domain.Reports;
using Reboard.Identity;
using Reboard.Repository;
using Reboard.Repository.Auth.Mongo;
using Reboard.Repository.Mongo;
using Reboard.Repository.Reports.Mongo;
using Reboard.Repository.Users.Mongo;
using Reboard.WebServer.Options;
using System.Text;

namespace Reboard.WebServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddCqrs(typeof(App.Reports.Register).Assembly)
                .AddCqrs(typeof(App.Users.Register).Assembly)
                .AddCors()
                .AddControllers();

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var secretKey = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddSingleton(_ => new MongoConnection(Configuration.GetValue("MongoConnection", ""), "reboard"));
            services.AddTransient<IRepository<Report>, MongoReportsRepository>();
            services.AddTransient<IUserRepository, MongoUserRepository>();
            services.AddTransient<IAuthRepository, MongoAuthRepository>();
            services.AddTransient<IUserService, RepositoryUserService>();
            services.AddTransient<IAuthService, RepositoryAuthService>();
            services.AddSingleton<IHashService, Sha256HashService>();
            services.AddSingleton<ITokenFactory>(_ => new JwtTokenFactory(appSettings.Secret));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseHttpsRedirection();
            }
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(x => x.MapControllers());
        }
    }
}