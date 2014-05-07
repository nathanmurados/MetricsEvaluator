using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetricsUtility.Core.ViewModels;

namespace MetricsUtility.Core.Services.Storers
{
    public class CssStatsStorer : ICssStatsStorer, IHasDateTimeProvider, IHasHumanInterface
    {
        public IHumanInterface Ux { get; private set; }
        public IDateTimeProvider DateTimeProvider { get; private set; }
        public IStorer Storer { get; private set; }
        public ICssStatsFileNameEvaluator CssStatsFileNameEvaluator { get; private set; }

        public CssStatsStorer(IStorer storer, IDateTimeProvider dateTimeProvider, IHumanInterface ux, ICssStatsFileNameEvaluator cssStatsFileNameEvaluator)
        {
            CssStatsFileNameEvaluator = cssStatsFileNameEvaluator;
            Ux = ux;
            DateTimeProvider = dateTimeProvider;
            Storer = storer;
        }

        /// <summary>
        /// Stores the results and returns the filename
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        public string Store(List<CssEvaluationResult> results)
        {
            var sb = new StringBuilder();

            //sb.AppendLine("Filename,PageInstances level LOC,Inline level instances,Inline level character count,Razor level instances,Razor level character count");
            sb.AppendLine("Filename,Block Level Instances,Block Level Lines Of Code,Inline Instances,Inline Character Count,Razor Instances, Razor Character Count");

            foreach (var result in results)
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

            var filename = Storer.Store(sb, CssStatsFileNameEvaluator.Evaluate());

            Ux.WriteLine(string.Format("Saved to {0}", filename));

            return filename;
        }
    }

    public interface ICssStatsStorer
    {
        string Store(List<CssEvaluationResult> results);
    }
}