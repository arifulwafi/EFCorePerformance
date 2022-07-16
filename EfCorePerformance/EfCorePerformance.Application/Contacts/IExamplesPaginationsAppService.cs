
namespace EfCorePerformance.Application.Contacts
{
    public interface IExamplesPaginationsAppService
    {
        Task<TestResult<int>> Bonus(int? salesPersonId, int? customerId, int? productId, bool includeProduct = false, int page = 10, int pageSize = 20, CancellationToken ct = default);
        Task<TestResult<int>> ExecutedOnDb(int salesPersonId = 1, int page = 10, int pageSize = 20, CancellationToken ct = default);
        Task<TestResult<int>> ExecutedOnDbSplitQuery(int salesPersonId = 1, int page = 10, int pageSize = 20, CancellationToken ct = default);
        Task<TestResult<int>> WorstCase(int salesPersonId = 1, int page = 10, int pageSize = 20, CancellationToken ct = default);
    }
}