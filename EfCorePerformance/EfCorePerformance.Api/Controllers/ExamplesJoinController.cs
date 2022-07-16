using EfCorePerformance.Application;
using EfCorePerformance.Application.Contacts;
using EfCorePerformance.Application.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EfCorePerformance.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExamplesJoinController : ControllerBase
    {
        private readonly IExamplesJoinAppService _service;

        public ExamplesJoinController(IExamplesJoinAppService service)
        {
            _service = service;
        }

        [HttpGet("worstCase")]
        public async Task<TestResult<int>> WorstCase(CancellationToken ct)
        {
            var result = await _service.WorstCase(ct);
            return result;
        }

        [HttpGet("badCase")]
        public async Task<TestResult<int>> BadCase(CancellationToken ct)
        {
            var result = await _service.BadCase(ct);
            return result;
        }

        [HttpGet("implicitJoin")]
        public async Task<TestResult<int>> ImplicitJoin(CancellationToken ct)
        {
            var result = await _service.ImplicitJoin(ct);
            return result;
        }

        [HttpGet("splitQuery")]
        public async Task<TestResult<int>> SplitQuery(CancellationToken ct)
        {
            var result = await _service.SplitQuery(ct);
            return result;
        }

        [HttpGet("splitQueryManualBad")]
        public async Task<TestResult<int>> SplitQueryManualBad(CancellationToken ct)
        {
            var result = await _service.SplitQueryManualBad(ct);
            return result;
        }

        [HttpGet("splitQueryManual")]
        public async Task<TestResult<int>> SplitQueryManual(CancellationToken ct)
        {
            var result = await _service.SplitQueryManual(ct);
            return result;
        }

        [HttpGet("complex")]
        public async Task<TestResult<int>> Complex(CancellationToken ct)
        {
            var result = await _service.Complex(ct);
            return result;
        }

        [HttpGet("complex-local-count")]
        public async Task<TestResult<int>> ComplexLocalCount(CancellationToken ct)
        {
            var result = await _service.ComplexLocalCount(ct);
            return result;
        }

        [HttpGet("complex-split-join")]
        public async Task<TestResult<int>> ComplexQueryJoin(CancellationToken ct)
        {
            var result = await _service.ComplexQueryJoin(ct);
            return result;
        }

        [HttpGet("complex-split-join-manual")]
        public async Task<TestResult<int>> ComplexSplitQueryManual(CancellationToken ct)
        {
            var result = await _service.ComplexSplitQueryManual(ct);
            return result;
        }

        /// <summary>
        /// Original query but with raw SQL.
        /// </summary>
        [HttpGet("rawSqlCommand")]
        public TestResult<int> RawSqlCommand()
        {
            var result = _service.RawSqlCommand();
            return result;
        }

        /// <summary>
        /// Improved raw SQL.
        /// </summary>
        [HttpGet("rawSqlCommandSplit")]
        public TestResult<int> RawSqlCommandSplit()
        {
            var result = _service.RawSqlCommandSplit();
            return result;
        }

        /// <summary>
        /// Attempting to optimize the raw SQL query #2.
        /// </summary>
        [HttpGet("rawSqlCommandSplitOnSql")]
        public TestResult<int> RawSqlCommandSplitOnSql()
        {
            var result = _service.RawSqlCommandSplitOnSql();
            return result;
        }

        /// <summary>
        /// Attempting to optimize the raw SQL query #3.
        /// </summary>
        [HttpGet("rawSqlCommandSplitOptimized")]
        public TestResult<int> RawSqlCommandSplitOptimized()
        {
            var result = _service.RawSqlCommandSplitOptimized();
            return result;
        }

        /// <summary>
        /// Best performance so far (replicating EF Core optimized code).
        /// </summary>
        [HttpGet("rawSqlCommandWithManualSplit")]
        public TestResult<int> RawSqlCommandWithManualSplit()
        {
            var result = _service.RawSqlCommandWithManualSplit();
            return result;
        }
    }
}
