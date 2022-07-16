
namespace EfCorePerformance.Application.Contacts
{
    public interface ITestAppService
    {
        bool Any();
        Task<int> AsyncCt(CancellationToken ct);
        int Count();
        Task<int> CountAsync();
        int Join();
        int Join2();
        int Join3();
        void MultiJoin();
        void MultiJoin2();
        void MultiJoin3();
        int NoTracking();
        int NoTracking2();
        int NoTracking3();
        int Tracking();
        bool WhereAny();
        bool WhereAnyFirst();
        int WhereCount();
    }
}