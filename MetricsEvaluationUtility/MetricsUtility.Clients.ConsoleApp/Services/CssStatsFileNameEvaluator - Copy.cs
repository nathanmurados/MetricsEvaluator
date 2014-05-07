using MetricsUtility.Core.Services;
using MetricsUtility.Core.Services.Storers;

namespace MetricsUtility.Clients.ConsoleApp.Services
{
    public class CssStatsFileNameEvaluator : ICssStatsFileNameEvaluator ,IHasDateTimeProvider
    {
        public IDateTimeProvider DateTimeProvider { get; private set; }

        public CssStatsFileNameEvaluator(IDateTimeProvider dateTimeProvider)
        {
            DateTimeProvider = dateTimeProvider;
        }
        
        public string Evaluate()
        {
            return "Css Validation Results " + DateTimeProvider.Now.ToString("yy-MM-dd HH.mm.ss") + ".csv";
        }
    }
}