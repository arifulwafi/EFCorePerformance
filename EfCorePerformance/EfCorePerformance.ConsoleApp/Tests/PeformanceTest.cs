using BenchmarkDotNet.Attributes;
using EfCorePerformance.Application.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCorePerformance.ConsoleApp.Tests
{
    public class PeformanceTest : IPeformanceTest
    {
        private readonly ITestAppService _testAppService;

        public PeformanceTest(ITestAppService testAppService)
        {
            _testAppService = testAppService;
        }

        [Benchmark]
        public int WhereCount()
        {
            var result = _testAppService.WhereCount();
            return result;
        }
    }
}
