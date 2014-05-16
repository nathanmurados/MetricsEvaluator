using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetricsUtility.Core.ViewModels;

namespace MetricsUtility.Core.Services.StorageServices
{
    public class CssStatsStorageService : ICssStatsStorageService, IHasDateTimeProvider, IHasHumanInterface
    {
        public IHumanInterface Ux { get; private set; }
        public IDateTimeProvider DateTimeProvider { get; private set; }
        public IStorageService StorageService { get; private set; }
        public ICssStatsFileNameEvaluator CssStatsFileNameEvaluator { get; private set; }

        public CssStatsStorageService(IStorageService storageService, IDateTimeProvider dateTimeProvider, IHumanInterface ux, ICssStatsFileNameEvaluator cssStatsFileNameEvaluator)
        {
            CssStatsFileNameEvaluator = cssStatsFileNameEvaluator;
            Ux = ux;
            DateTimeProvider = dateTimeProvider;
            StorageService = storageService;
        }

        /// <summary>
        /// Stores the results and returns the filename
        /// </summary>
        /// <param name="results"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public string Store(List<CssEvaluationResult> results, string groupName)
        {
            var sb = new StringBuilder();

            //sb.AppendLine("Filename,PageInstances level LOC,Inline level instances,Inline level character count,Razor level instances,Razor level character count");
            sb.AppendLine("Filename,Block Level Instances,Block Level Lines Of Code,Inline Instances,Inline Character Count,Razor Instances, Razor Character Count");

            foreach (var result in results.OrderBy(x=>x.FileName))
            {
                sb.AppendLine(string.Format("{0},{1},{2},{3},{4},{5},{6}",
                    result.FileName,
                    result.Page.Count,
                    result.Page.Sum(x => x),
                    result.Inline.Count,
                    result.Inline.Sum(x => x.Value.Length),
                    result.Razor.Count,
                    result.Razor.Sum(x => x.Value.Length)
                ));
            }

            sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6}", 
                results.Count, 
                results.Sum(x => x.Page.Count),
                results.Sum(x => x.Page.Sum(y=>y)),
                results.Sum(x => x.Inline.Count),
                results.Sum(x => x.Inline.Sum(y => y.Value.Length)),
                results.Sum(x => x.Razor.Count),
                results.Sum(x => x.Razor.Sum(y => y.Value.Length))
            );

            var filename = StorageService.Store(sb, CssStatsFileNameEvaluator.Evaluate(groupName));

            Ux.WriteLine(string.Format("Saved to {0}", filename));
            Ux.WriteLine("");

            return filename;
        }
    }
}