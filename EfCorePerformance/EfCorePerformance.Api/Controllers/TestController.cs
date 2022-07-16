using EfCorePerformance.Application.Contacts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EfCorePerformance.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ITestAppService _service;

        public TestController(ITestAppService service)
        {
            _service = service;
        }

        [HttpGet("where-count")]
        public int WhereCount()
        {
            var result = _service.WhereCount();
            return result;
        }

        [HttpGet("count")]
        public int Count()
        {
            var result = _service.Count();
            return result;
        }

        [HttpGet("countasync")]
        public async Task<int> CountAsync()
        {
            var result = await _service.CountAsync();
            return result;
        }

        [HttpGet("countasync-ct")]
        public async Task<int> AsyncCt(CancellationToken ct)
        {
            var result = await _service.AsyncCt(ct);
            return result;
        }

        [HttpGet("tracking")]
        public int Tracking()
        {
            var result = _service.Tracking();
            return result;
        }

        [HttpGet("notracking")]
        public int NoTracking()
        {
            var result = _service.NoTracking();
            return result;
        }

        [HttpGet("notracking2")]
        public int NoTracking2()
        {
            var result = _service.NoTracking2();
            return result;
        }

        [HttpGet("notracking3")]
        public int NoTracking3()
        {
            var result = _service.NoTracking3();
            return result;
        }

        [HttpGet("join")]
        public int Join()
        {
            var result = _service.Join();
            return result;
        }

        [HttpGet("join2")]
        public int Join2()
        {
            var result = _service.Join2();
            return result;
        }

        [HttpGet("join3")]
        public int Join3()
        {
            var result = _service.Join3();
            return result;
        }

        [HttpGet("nosplit-join")]
        public void MultiJoin()
        {
            _service.MultiJoin();
        }

        [HttpGet("split-join")]
        public void MultiJoin2()
        {
            _service.MultiJoin2();
        }

        [HttpGet("split-join-manual")]
        public void MultiJoin3()
        {
            _service.MultiJoin3();
        }

        [HttpGet("where-any-list")]
        public bool WhereAny()
        {
            var result = _service.WhereAny();
            return result;
        }

        [HttpGet("where-any-first")]
        public bool WhereAnyFirst()
        {
            var result = _service.WhereAnyFirst();
            return result;
        }

        [HttpGet("any")]
        public bool Any()
        {
            var result = _service.Any();
            return result;
        }
    }
}
