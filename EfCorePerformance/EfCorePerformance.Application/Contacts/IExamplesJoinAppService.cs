
namespace EfCorePerformance.Application.Contacts
{
    public interface IExamplesJoinAppService
    {
        Task<TestResult<int>> BadCase(CancellationToken ct);
        Task<TestResult<int>> Complex(CancellationToken ct);
        Task<TestResult<int>> ComplexLocalCount(CancellationToken ct);
        Task<TestResult<int>> ComplexQueryJoin(CancellationToken ct);
        Task<TestResult<int>> ComplexSplitQueryManual(CancellationToken ct);
        Task<TestResult<int>> ImplicitJoin(CancellationToken ct);
        TestResult<int> RawSqlCommand();
        TestResult<int> RawSqlCommandSplit();
        TestResult<int> RawSqlCommandSplitOnSql();
        TestResult<int> RawSqlCommandSplitOptimized();
        TestResult<int> RawSqlCommandWithManualSplit();
        Task<TestResult<int>> SplitQuery(CancellationToken ct);
        Task<TestResult<int>> SplitQueryManual(CancellationToken ct);
        Task<TestResult<int>> SplitQueryManualBad(CancellationToken ct);
        Task<TestResult<int>> WorstCase(CancellationToken ct);
    }
}