﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
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
using Reboard.WebServer.Architecture;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Threading.Tasks;

namespace Reboard.WebServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddCqrs(typeof(App.Reports.Register).Assembly)
                .AddCqrs(typeof(App.Users.Register).Assembly)
                .AddCors(options =>
                {
                    options.AddDefaultPolicy(policy =>
                    {
                        policy
                            .WithOrigins("http://localhost:3000")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .WithExposedHeaders("Location");
                    });
                })
                .AddTransient<IUserAccessor, UserAccessor>()
                .AddTransient<IWsProvider, WsProvider>()
                .AddTransient<INotification, WsProvider>()
                .AddSingleton<IQueryDispatcher, DefaultQueryDispatcher>()
                .AddSingleton<ICommandDispatcher, DefaultCommandDispatcher>()
                .AddSingleton<InMemoryQueueCommandDispatcher>()
                .AddSingleton<IQueueCommandDispatcher>(sp => 
                    new WsQueueCommandDispatcher(
                        sp.GetRequiredService<InMemoryQueueCommandDispatcher>(), 
                        sp.GetRequiredService<IHttpContextAccessor>(), 
                        sp.GetRequiredService<INotification>()))

                .AddControllers()
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
            services.AddSingleton(_ => new MongoConnection(Configuration.GetValue("MongoConnection", ""), "reboard"));
            services.AddTransient<IRepository<Report>, MongoReportsRepository>();
            services.AddTransient<IUserRepository, MongoUserRepository>();
            services.AddTransient<IAuthRepository, MongoAuthRepository>();
            services.AddTransient<IUserService, RepositoryUserService>();
            services.AddTransient<IAuthService, RepositoryAuthService>();
            services.AddSingleton<IHashService, Sha256HashService>();
            services.AddSingleton<ITokenFactory>(_ => new JwtTokenFactory(appSettings.Secret));
            services.AddSingleton<IUniqueIdFactory, MongoUniqueIdFactory>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseCors();
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
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseWebSockets();
            app.UseEndpoints(x => x.MapControllers());
        }
    }
}