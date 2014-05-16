using MetricsUtility.Core.Services;
using MetricsUtility.Core.Services.StorageServices;

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
            return string.Format("Css Results {0}.csv", DateTimeProvider.Now.ToString("yy-MM-dd HH.mm.ss"));
        }

        public string Evaluate(string groupName)
        {
            return string.Format("Css Results - {0} {1}.csv", 
                groupName,
                DateTimeProvider.Now.ToString("yy-MM-dd HH.mm.ss"));
        }
    }
}