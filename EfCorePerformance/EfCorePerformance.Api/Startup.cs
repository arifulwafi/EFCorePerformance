using EfCorePerformance.Application.Contacts;
using EfCorePerformance.Application.Persistence;
using EfCorePerformance.Application.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfCorePerformance.Api
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EfCorePerformance.Api", Version = "v1" });
            });

            services.AddTransient<IExamplesAppService, ExamplesAppService>();
            services.AddTransient<IExamplesJoinAppService, ExamplesJoinAppService>();
            services.AddTransient<IExamplesPaginationsAppService, ExamplesPaginationsAppService>();
            services.AddTransient<ITestAppService, TestAppService>();

            //            // By default we are adding SQL Server DB context.
            services.AddDbContextPool<SalesDbContext>(options =>
            {
                // You can also use SQL Server.
                options.UseSqlServer(Configuration.GetConnectionString("SalesDB"));

#if DEBUG
                // Most project shouldn't expose sensitive data, which is why we are
                // limiting to be available only in DEBUG mode.
                // If this is not, SQL "parameters" will be '?' instead of actual values.
                options.EnableSensitiveDataLogging();
#endif
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EfCorePerformance.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
