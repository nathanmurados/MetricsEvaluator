using MetricsUtility.Core.Services;
using MetricsUtility.Core.Services.Storers;

namespace MetricsUtility.Clients.Wpf.Services.Evaluators
{
    public class CssStatsFileNameEvaluator : ICssStatsFileNameEvaluator, IHasDateTimeProvider
    {
        public IDateTimeProvider DateTimeProvider { get; private set; }

        public CssStatsFileNameEvaluator(IDateTimeProvider dateTimeProvider)
        {
            DateTimeProvider = dateTimeProvider;
        }

        public string Evaluate()
        {
            return string.Format("CSS Results {0} {1}.csv", Properties.Settings.Default.InspectionPath.Replace("\\", "~").Replace(":", ""), DateTimeProvider.Now.ToString("yyMMddHHmmss"));
        }
    }
}