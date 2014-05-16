using MetricsUtility.Core.Services;
using MetricsUtility.Core.Services.StorageServices;

namespace MetricsUtility.Clients.Wpf.Services.Evaluators
{
    public class JavaScriptStatsFileNameEvaluator : IJavaScriptStatsFileNameEvaluator, IHasDateTimeProvider
    {
        public IDateTimeProvider DateTimeProvider { get; private set; }

        public JavaScriptStatsFileNameEvaluator(IDateTimeProvider dateTimeProvider)
        {
            DateTimeProvider = dateTimeProvider;
        }
        
        public string Evaluate(string groupName)
        {
            return string.Format("JS Results {0}{1}.csv",
                DateTimeProvider.Now.ToString("yyMMddHHmmss"),
                string.IsNullOrWhiteSpace(groupName) ? "" : string.Format(" {0} ", groupName)
                //Properties.Settings.Default.InspectionPath.Replace("\\", "~").Replace(":", ""),
            );
        }
    }
}