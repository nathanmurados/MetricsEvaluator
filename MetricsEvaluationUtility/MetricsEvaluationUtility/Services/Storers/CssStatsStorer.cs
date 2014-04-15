using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetricsEvaluationUtility.ViewModels;

namespace MetricsEvaluationUtility.Services.Storers
{
    public class CssStatsStorer : ICssStatsStorer, IHasDateTimeProvider, IHasHumanInterface
    {
        public IHumanInterface Ux { get; private set; }
        public IDateTimeProvider DateTimeProvider { get; private set; }
        public IStorer Storer { get; private set; }

        public CssStatsStorer(IStorer storer, IDateTimeProvider dateTimeProvider, IHumanInterface ux)
        {
            Ux = ux;
            DateTimeProvider = dateTimeProvider;
            Storer = storer;
        }

        public void Store(List<CssEvaluationResult> results)
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

            Storer.Store(sb, "CssValidationResults " + DateTimeProvider.Now.ToString("yy-MM-dd HH.mm.ss") + ".csv");

            Ux.WriteLine("Saved.");
        }
    }

    public interface ICssStatsStorer
    {
        void Store(List<CssEvaluationResult> results);
    }
}