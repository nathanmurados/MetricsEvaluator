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
            return "Css Results " + Properties.Settings.Default.InspectionPath.Replace("\\", "~").Replace(":", "") + DateTimeProvider.Now.ToString("yy-MM-dd HH.mm.ss") + ".csv";
        }
    }
}