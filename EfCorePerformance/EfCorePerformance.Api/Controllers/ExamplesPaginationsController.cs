using EfCorePerformance.Application;
using EfCorePerformance.Application.Contacts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EfCorePerformance.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExamplesPaginationsController : Controller
    {
        private readonly IExamplesPaginationsAppService _service;
        private const int DefaultPageIndex = 10;
        private const int DefaultPageSize = 20;
        private const int DefaultSalesPersonId = 1;

        public ExamplesPaginationsController(IExamplesPaginationsAppService service)
        {
            _service = service;
        }

        [HttpGet("worstCase")]
        public async Task<TestResult<int>> WorstCase(int salesPersonId = DefaultSalesPersonId, int page = DefaultPageIndex, int pageSize = DefaultPageSize, CancellationToken ct = default)
        {
            var result = await _service.WorstCase(salesPersonId, page, page, ct);
            return result;
        }

        [HttpGet("executedOnDB")]
        public async Task<TestResult<int>> ExecutedOnDb(int salesPersonId = DefaultSalesPersonId, int page = DefaultPageIndex, int pageSize = DefaultPageSize, CancellationToken ct = default)
        {
            var result = await _service.ExecutedOnDb(salesPersonId, page, page, ct);
            return result;
        }

        [HttpGet("executedOnDBSplitQuery")]
        public async Task<TestResult<int>> ExecutedOnDbSplitQuery(int salesPersonId = DefaultSalesPersonId, int page = DefaultPageIndex, int pageSize = DefaultPageSize, CancellationToken ct = default)
        {
            var result = await _service.ExecutedOnDbSplitQuery(salesPersonId, page, page, ct);
            return result;
        }

        [HttpGet("bonus")]
        public async Task<TestResult<int>> Bonus(int? salesPersonId, int? customerId, int? productId, bool includeProduct = false, int page = DefaultPageIndex, int pageSize = DefaultPageSize, CancellationToken ct = default)
        {
            var result = await _service.Bonus(salesPersonId, customerId, productId, includeProduct, page, page, ct);
            return result;
        }
    }
}
