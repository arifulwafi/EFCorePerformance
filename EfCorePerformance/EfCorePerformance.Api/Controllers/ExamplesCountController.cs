using EfCorePerformance.Application;
using EfCorePerformance.Application.Contacts;
using EfCorePerformance.Application.Persistence;
using EfCorePerformance.Application.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;

namespace EfCorePerformance.Api.Controllers
{
    /// <summary>
    /// Testing pure performance and skipped on CancellationToken and Async.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ExamplesCountController : ControllerBase
    {
        private readonly IExamplesAppService _service;

        public ExamplesCountController(IExamplesAppService service)
        {
            _service = service;
        }

        [HttpGet("worstCase")]
        public TestResult<int> WorstCase(bool isLoadFriendly = false)
        {
            return _service.WorstCase(isLoadFriendly);
        }

        /// <summary>
        /// Almost worst case scenario with no tracking.
        /// </summary>
        [HttpGet("worstCaseNoTracking")]
        public TestResult<int> WorstCaseNoTracking(bool isLoadFriendly = false)
        {
            return _service.WorstCaseNoTracking(isLoadFriendly);
        }

        /// <summary>
        /// Almost worst case scenario with no tracking.
        /// </summary>
        [HttpGet("worstCaseNoTrackingIdOnly")]
        public TestResult<int> WorstCaseNoTrackingIdOnly(bool isLoadFriendly = false)
        {
            return _service.WorstCaseNoTrackingIdOnly(isLoadFriendly);
        }

        /// <summary>
        /// Almost worst case scenario with no tracking.
        /// </summary>
        [HttpGet("bestCase")]
        public TestResult<int> BestCase(bool isLoadFriendly = false)
        {
            return _service.BestCase(isLoadFriendly);
        }

        /// <summary>
        /// Almost worst case scenario with no tracking.
        /// </summary>
        [HttpGet("rawSql")]
        public TestResult<int> RawSql(bool isLoadFriendly = false)
        {

            return _service.RawSql(isLoadFriendly);
        }

        /// <summary>
        /// Almost worst case scenario with no tracking.
        /// </summary>
        [HttpGet("rawSqlCommand")]
        public TestResult<int> RawSqlCommand(bool isLoadFriendly = false)
        {
            return _service.RawSqlCommand(isLoadFriendly);
        }
    }
}
