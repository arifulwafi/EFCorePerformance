using EfCorePerformance.Application.Contacts;
using EfCorePerformance.Application.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EfCorePerformance.Application.Services
{
    public class TestAppService : ITestAppService
    {
        private readonly SalesDbContext _dbContext;

        public TestAppService(SalesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int WhereCount()
        {
            var sales = _dbContext.Sales
                .Where(x => x.Quantity < 100)
                .ToList();

            return sales.Count;
        }

        public int Count()
        {
            return _dbContext.Sales.Count(x => x.Quantity < 100);
        }

        public async Task<int> CountAsync()
        {
            return await _dbContext.Sales.CountAsync(x => x.Quantity < 100);
        }

        public async Task<int> AsyncCt(CancellationToken ct)
        {
            return await _dbContext.Sales.CountAsync(x => x.Quantity < 100, ct);
        }

        public int Tracking()
        {
            return _dbContext.Sales
                .Where(x => x.Quantity < 100)
                .ToList()
                .Count;
        }

        public int NoTracking()
        {
            return _dbContext.Sales
                .AsNoTracking()
                .Where(x => x.Quantity < 100)
                .ToList()
                .Count;
        }

        public int NoTracking2()
        {
            return _dbContext.Sales
                .AsNoTracking()
                .Where(x => x.Quantity < 100)
                .Select(x => x.SalesId)
                .ToList()
                .Count;
        }

        public int NoTracking3()
        {
            return _dbContext.Sales
                .AsNoTracking()
                .Where(x => x.Quantity < 100)
                .Select(x => new
                {
                    x.ProductId,
                    x.Quantity,
                    x.SalesId,
                    x.SalesPersonId,
                    x.CustomerId
                })
                .ToList()
                .Count;
        }

        public int Join()
        {
            return _dbContext.Sales
                .AsNoTracking()
                .Where(x => x.SalesPersonId == 1)
                .Select(x => new
                {
                    x.ProductId,
                    x.Quantity,
                    x.SalesId,
                    x.SalesPersonId,
                    SalesPerson = x.SalesPerson.FirstName,
                })
                .ToList()
                .Count;
        }

        public int Join2()
        {
            return _dbContext.Sales
                .AsNoTracking()
                .AsSplitQuery()
                .Include(x => x.SalesPerson)
                .Where(x => x.SalesPersonId == 1)
                .Select(x => new
                {
                    x.ProductId,
                    x.Quantity,
                    x.SalesId,
                    x.SalesPersonId,
                    SalesPerson = x.SalesPerson.FirstName,
                })
                .ToList()
                .Count;
        }

        public int Join3()
        {
            string salesPerson = _dbContext.Employees
                .AsNoTracking()
                .Where(x => x.EmployeeId == 1)
                .Select(x => x.FirstName)
                .First();
            return _dbContext.Sales
                .AsNoTracking()
                .AsSplitQuery()
                .Where(x => x.SalesPersonId == 1)
                .Select(x => new
                {
                    x.ProductId,
                    x.Quantity,
                    x.SalesId,
                    x.SalesPersonId
                })
                .ToList()
                .Select(x => new
                {
                    x.ProductId,
                    x.Quantity,
                    x.SalesId,
                    x.SalesPersonId,
                    SalesPerson = salesPerson,
                })
                .ToList()
                .Count;
        }

        public void MultiJoin()
        {
            var result = _dbContext.Employees
                .AsNoTracking()
                .Include(x => x.Sales)
                .Where(x => x.EmployeeId == 1)
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    SoldQuantity = x.Sales.Count,
                    x.Sales
                })
                .FirstOrDefault();
        }

        public void MultiJoin2()
        {
            var result = _dbContext.Employees
                .AsNoTracking()
                .AsSplitQuery()
                .Where(x => x.EmployeeId == 1)
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    SoldQuantity = x.Sales.Count,
                    x.Sales
                })
                .FirstOrDefault();
        }

        public void MultiJoin3()
        {
            var salesPerson = _dbContext.Employees
                .AsNoTracking()
                .Where(x => x.EmployeeId == 1)
                .Select(x => new
                {
                    x.EmployeeId,
                    x.FirstName,
                    x.LastName,
                    SalesId = x.Sales.Select(x => x.SalesId)
                })
                .FirstOrDefault();

            var sales = _dbContext.Sales
                .AsNoTracking()
                .Where(x => x.SalesPersonId == salesPerson.EmployeeId)
                .Select(x => new
                {
                    x.SalesId,
                    x.Quantity,
                    x.ProductId,
                    x.CustomerId
                })
                .FirstOrDefault();
        }

        public bool WhereAny()
        {
            var sales = _dbContext.Sales
                .AsNoTracking()
                .Where(x => x.SalesPersonId == 1)
                .ToList();

            return sales.Any();
        }

        public bool WhereAnyFirst()
        {
            var sales = _dbContext.Sales
                .AsNoTracking()
                .Where(x => x.SalesPersonId == 1)
                .FirstOrDefault();

            return sales != null;
        }

        public bool Any()
        {
            return _dbContext.Sales
                .AsNoTracking()
                .Where(x => x.SalesPersonId == 1)
                .Any();
        }
    }
}
