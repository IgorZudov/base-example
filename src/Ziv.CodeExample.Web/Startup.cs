using System;
using System.IO;
using System.Linq;
using AutoMapper;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Ziv.CodeExample.Database;
using Microsoft.Extensions.Logging;
using Ziv.CodeExample.Web.Controllers;
using Ziv.CodeExample.Web.Mapping;
using IApplicationLifetime = Microsoft.Extensions.Hosting.IApplicationLifetime;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Ziv.CodeExample.Web
{
    public class Startup
    {
        private readonly string _basePath;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _basePath = string.IsNullOrWhiteSpace(Configuration["Endpoint"]) ? "" : $"/{Configuration["Endpoint"].Trim('/')}";
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ControllerMappingProfile());
            }).CreateMapper());

            services.AddHealthChecks();
            
            services.AddDomain();
			services.AddDatabase(options => options.UseNpgsql(Configuration.GetConnectionString("Db")));
            
            services.AddMvc()
                .AddControllersAsServices();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info() { 
                Title = "Ziv.CodeExample.Domain",
                Version = "v1".Trim(),
            });
                var xmlFile = $"{typeof(UserController).Assembly.GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        [UsedImplicitly]
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime,
            ILogger<Startup> logger, IServiceProvider serviceProvider)
        { 
            LogLifetime(lifetime, logger);
            app.UseExceptionHandler(logger);
            app.UsePathBase(new PathString(_basePath));
            
            app.UseHealthChecks("/health");
            if (env.IsDevelopment())
            {
               app.UseDeveloperExceptionPage();
            }
            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swagger, httpReq) =>
                {
                    swagger.Host = httpReq.Host.Value;
                    swagger.Paths = swagger.Paths.ToDictionary(p => _basePath + p.Key, p => p.Value);
                    swagger.BasePath = "";
                });
                c.RouteTemplate = "/swagger/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(c => { c.SwaggerEndpoint($"{_basePath}/swagger/v1/swagger.json", "Api V1"); });

            app.UseMvc();
        }

        private static void LogLifetime(IApplicationLifetime lifetime, ILogger logger)
        {
            lifetime.ApplicationStarted.Register(() => logger.LogInformation("Service started"));
            lifetime.ApplicationStopped.Register(() => logger.LogInformation("Service stopped"));
        }
    }
}