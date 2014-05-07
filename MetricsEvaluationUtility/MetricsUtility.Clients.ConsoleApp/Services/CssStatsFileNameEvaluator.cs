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
        
        public string Evaluate()
        {
            return "JS Validation Results " + DateTimeProvider.Now.ToString("yy-MM-dd HH.mm.ss") + ".csv";
        }
    }
}