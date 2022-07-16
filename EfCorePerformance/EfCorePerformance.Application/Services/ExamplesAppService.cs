using EfCorePerformance.Application.Contacts;
using EfCorePerformance.Application.Persistence;
using EfCorePerformance.Application.Utils;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;

namespace EfCorePerformance.Application.Services
{
    public class ExamplesAppService : IExamplesAppService
    {
        private readonly SalesDbContext _dbContext;

        public ExamplesAppService(SalesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TestResult<int> WorstCase(bool isLoadFriendly = false)
        {
            IQueryable<Sale> query = GetBaseQuery(isLoadFriendly);
            return new TestResult<int>
            {
                Sql = query.ToQueryString(),
                LiveSql = true,
                Result = query
                    .TagWithContext()
                    .ToList()
                    .Count
            };
        }

        /// <summary>
        /// Almost worst case scenario with no tracking.
        /// </summary>
        public TestResult<int> WorstCaseNoTracking(bool isLoadFriendly = false)
        {
            IQueryable<Sale> query = GetBaseQuery(isLoadFriendly);
            return new TestResult<int>
            {
                Sql = query.ToQueryString(),
                LiveSql = true,
                Result = query
                    .AsNoTracking()
                    .TagWithContext()
                    .ToList()
                    .Count
            };
        }

        /// <summary>
        /// Almost worst case scenario with no tracking.
        /// </summary>
        public TestResult<int> WorstCaseNoTrackingIdOnly(bool isLoadFriendly = false)
        {
            IQueryable<Sale> query = GetBaseQuery(isLoadFriendly);
            return new TestResult<int>
            {
                Sql = query.ToQueryString(),
                LiveSql = true,
                Result = query
                    .AsNoTracking()
                    .Select(x => x.SalesId)
                    .TagWithContext()
                    .ToList()
                    .Count
            };
        }

        /// <summary>
        /// Almost worst case scenario with no tracking.
        /// </summary>
        public TestResult<int> BestCase(bool isLoadFriendly = false)
        {
            IQueryable<Sale> query = GetBaseQuery(isLoadFriendly);
            return new TestResult<int>
            {
                Sql = "SELECT COUNT(*) FROM[Sales] AS[s]",
                LiveSql = false,
                Result = query.TagWithContext().Count()
            };
        }

        /// <summary>
        /// Almost worst case scenario with no tracking.
        /// </summary>
        public TestResult<int> RawSql(bool isLoadFriendly = false)
        {

            return new TestResult<int>
            {
                // Despite running raw SQL, EF Core will add a select statement to map out
                Sql = "SELECT COUNT(*) as Count FROM [Sales]",
                LiveSql = false,

                // NOTE: Might not be the best practice but it works.
                // Counts is a is a non-existing view view data structure we are looking for.
                // HACK: We use first `.ToList()` because if use `.FirstOrDefault()` it will add an additional "SELECT TOP(1) [c].[Count] FROM( ... ) AS [c]"
                // Then we select Count and FirstOrDefault in-memory.
                Result = _dbContext.Counts
                    .FromSqlRaw("SELECT COUNT(*) as Count FROM [Sales]")
                    .ToList()
                    .Select(x => x.Count)
                    .FirstOrDefault()
            };
        }

        /// <summary>
        /// Almost worst case scenario with no tracking.
        /// </summary>
        public TestResult<int> RawSqlCommand(bool isLoadFriendly = false)
        {
            int count;
            using (var command = _dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT COUNT(*) FROM [Sales]";
                if (isLoadFriendly)
                {
                    command.CommandText += " where [Quantity] < 100";
                }

                command.CommandType = CommandType.Text;

                _dbContext.Database.OpenConnection();
                using System.Data.Common.DbDataReader result = command.ExecuteReader();
                result.Read();
                count = result.GetInt32(0);
            }

            return new TestResult<int>
            {
                Sql = "SELECT COUNT(*) FROM [Sales]",
                LiveSql = false,
                Result = count
            };
        }

        private IQueryable<Sale> GetBaseQuery(bool isLoadFriendly = false)
            => !isLoadFriendly
            ? _dbContext.Sales
            : _dbContext.Sales
                .Where(x => x.Quantity < 100);
    }
}
