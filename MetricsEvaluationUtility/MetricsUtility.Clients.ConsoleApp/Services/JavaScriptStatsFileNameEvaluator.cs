using MetricsUtility.Core.Services;
using MetricsUtility.Core.Services.Storers;

namespace MetricsUtility.Clients.ConsoleApp.Services
{
    public class JavaScriptStatsFileNameEvaluator : IJavaScriptStatsFileNameEvaluator ,IHasDateTimeProvider
    {
        public IDateTimeProvider DateTimeProvider { get; private set; }

        public JavaScriptStatsFileNameEvaluator(IDateTimeProvider dateTimeProvider)
        {
            DateTimeProvider = dateTimeProvider;
        }

        public string Evaluate(string groupName)
        {
            return string.Format("JavaScript Results - {0} {1}.csv",
                groupName,
                DateTimeProvider.Now.ToString("yy-MM-dd HH.mm.ss"));
        }
    }
}