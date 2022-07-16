using BenchmarkDotNet.Running;
using EfCorePerformance.Application.Contacts;
using EfCorePerformance.Application.Persistence;
using EfCorePerformance.Application.Services;
using EfCorePerformance.ConsoleApp.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World!");

        var serviceProvider = new ServiceCollection()
            .AddLogging()
            .AddDbContextPool<SalesDbContext>(options =>
            {
                // You can also use SQL Server.
                options.UseSqlServer("data source=.;initial catalog=salesdb;integrated security=True;multipleactiveresultsets=True;");

#if DEBUG
                // Most project shouldn't expose sensitive data, which is why we are
                // limiting to be available only in DEBUG mode.
                // If this is not, SQL "parameters" will be '?' instead of actual values.
                options.EnableSensitiveDataLogging();
#endif
            })
                .AddTransient<IExamplesAppService, ExamplesAppService>()
                .AddTransient<IExamplesJoinAppService, ExamplesJoinAppService>()
                .AddTransient<IExamplesPaginationsAppService, ExamplesPaginationsAppService>()
                .AddTransient<ITestAppService, TestAppService>()
                // Console test files
                .AddTransient<IPeformanceTest, PeformanceTest>()
            .BuildServiceProvider();

        //do the actual work here
        //var peformanceTest = serviceProvider.GetService<IPeformanceTest>();
        //var result = peformanceTest.WhereCount();

        var summary = BenchmarkRunner.Run(typeof(Program).Assembly);


    }

}
