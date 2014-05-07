using MetricsUtility.Core.Services;
using MetricsUtility.Core.Services.Storers;

namespace MetricsUtility.Clients.Wpf.Services.Evaluators
{
    public class JavaScriptStatsFileNameEvaluator : IJavaScriptStatsFileNameEvaluator, IHasDateTimeProvider
    {
        public IDateTimeProvider DateTimeProvider { get; private set; }

        public JavaScriptStatsFileNameEvaluator(IDateTimeProvider dateTimeProvider)
        {
            DateTimeProvider = dateTimeProvider;
        }

        public string Evaluate()
        {
            return string.Format("JS Results {0} {1}.csv", Properties.Settings.Default.InspectionPath.Replace("\\", "~").Replace(":", ""), DateTimeProvider.Now.ToString("yyMMddHHmmss"));
        }
    }
}