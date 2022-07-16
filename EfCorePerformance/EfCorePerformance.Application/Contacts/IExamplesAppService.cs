namespace EfCorePerformance.Application.Contacts
{
    public interface IExamplesAppService
    {
        TestResult<int> BestCase(bool isLoadFriendly = false);
        TestResult<int> RawSql(bool isLoadFriendly = false);
        TestResult<int> RawSqlCommand(bool isLoadFriendly = false);
        TestResult<int> WorstCase(bool isLoadFriendly = false);
        TestResult<int> WorstCaseNoTracking(bool isLoadFriendly = false);
        TestResult<int> WorstCaseNoTrackingIdOnly(bool isLoadFriendly = false);
    }
}