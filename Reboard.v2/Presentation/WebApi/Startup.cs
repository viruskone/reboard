using Hellang.Middleware.ProblemDetails;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Reboard.Core.Application.Identity;
using Reboard.Core.Application.Reports;
using Reboard.Core.Application.Users;
using Reboard.Core.Application.Reports;
using Reboard.Core.Domain.Base;
using Reboard.Core.Domain.Base.Rules;
using Reboard.Core.Domain.Users.OutboundServices;
using Reboard.Infrastructure.Identity;
using Reboard.Infrastructure.MongoDB;
using Reboard.Presentation.WebApi.Exceptions;
using Reboard.Presentation.WebApi.Options;
using Reboard.Presentation.WebApi.Users;
using Reboard.Presentation.WebApi.Infrastructure;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Reboard.Presentation.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
            }
            else
            {
                app.UseHttpsRedirection();
            }

            app.UseProblemDetails();

            app.UseRouting();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseWebSockets();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Reboard WebApi", Version = "v1" });
                c.EnableAnnotations();
            });
            services.AddCors(options =>
                {
                    options.AddDefaultPolicy(policy =>
                    {
                        policy
                            .WithOrigins("http://localhost:3000")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .WithExposedHeaders("Location");
                    });
                });
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.Converters.Add(new TimeSpanToStringConverter());
                });
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
                x.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Headers.ContainsKey("sec-websocket-protocol") && context.HttpContext.WebSockets.IsWebSocketRequest)
                        {
                            var protocols = context.Request.Headers["sec-websocket-protocol"].ToString().Split(", ");
                            context.Request.Headers["sec-websocket-protocol"] = protocols[0];
                            context.Token = protocols[1];
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddProblemDetails(x =>
            {
                x.Map<BusinessRuleValidationException>(ex => new BusinessRuleValidationExceptionProblemDetails(ex));
                x.Map<ConstructDomainObjectRuleValidationException>(ex => new ConstructDomainObjectRuleValidationExceptionProblemDetails(ex));
            });

            services.AddMediatR(typeof(Core.Application.Users.EntryPoint).Assembly);
            services.AddMediatR(typeof(Core.Application.Reports.EntryPoint).Assembly);
            services.AddHttpContextAccessor();
            // user defined
            services.AddUserApplication();
            services.AddReportApplication();
            services.AddMongoDbRepositories(Configuration.GetValue("MongoConnection", ""), "reboard");
            services.AddSingleton<ITokenFactory>(_ => new JwtTokenFactory(appSettings.Secret));
            services.AddSingleton<IHashService, Sha256HashService>();

            services.AddTransient<IUserAccessor, JwtUserAccessor>();
        }
    }
}