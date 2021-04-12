using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text.Json;
using Catalog_API.Repositories;
using Catalog_API.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Catalog_API
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MongoDbSettings>(_config.GetSection(nameof(MongoDbSettings)));
            services.AddScoped<IItemRepository, MongoItemRepository>();
            services.AddControllers();
            var settings = _config.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
            services.AddHealthChecks()
                    .AddMongoDb(
                        settings.ConnectionString,
                        name: "mongodb",
                        timeout: TimeSpan.FromSeconds(3),
                        tags: new[] { "ready" }
                    );
            services.AddSwaggerGen(c => 
            {
                c.SwaggerDoc(name: "v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "Catalog API",
                    Description = "A simple RESTful Web API",
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }
            else
            {
                app.UseHsts();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}/{id?}"
                );
                endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions()
                {
                    Predicate = check => check.Tags.Contains("ready"),
                    ResponseWriter = async (context, report) => {
                        var result = JsonSerializer.Serialize(new
                        {
                            status = report.Status.ToString(),
                            checks = report.Entries.Select(entry => new
                            {
                                name = entry.Key,
                                status = entry.Value.Status.ToString(),
                                exception = entry.Value.Exception?.Message ?? "none",
                                duration = entry.Value.Duration.ToString()
                            })
                        });
                        context.Response.ContentType = MediaTypeNames.Application.Json;
                        await context.Response.WriteAsync(result);
                    }
                });
                // Check if API is up and running
                // Note: if Options isn't provided it will do all checks including Mongodb
                endpoints.MapHealthChecks("/health/alive", new HealthCheckOptions()
                {
                    Predicate = (_) => false
                });
            });
        }
    }
}
