using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MetricsUtility.Core.Services.Evaluators.Css;
using MetricsUtility.Core.Services.Storers;
using MetricsUtility.Core.ViewModels;

namespace MetricsUtility.Core.Services.Presenters
{
    public class CssStatsPresenter : ICssStatsPresenter
    {
        public IHumanInterface Ux { get; private set; }
        public ICssValidationEvaluator CssValidationEvaluator { get; private set; }
        public ICssStatsStorer CssStatsStorer { get; private set; }

        public CssStatsPresenter(IHumanInterface ux, ICssValidationEvaluator cssValidationEvaluator, ICssStatsStorer cssStatsStorer)
        {
            CssStatsStorer = cssStatsStorer;
            CssValidationEvaluator = cssValidationEvaluator;
            Ux = ux;
        }

        public List<CssEvaluationResult> Present(List<string> files)
        {
            var results = new List<CssEvaluationResult>();

            var i = 0;
            var count = files.Count();
            double oldPercentage = 0;

            var lockTarget = new object();

            Parallel.ForEach(files, file =>
            {
                var contents = File.ReadAllLines(file);

                var result = CssValidationEvaluator.Evaluate(file, contents);
                if (result != null)
                {
                    results.Add(result);
                }

                Interlocked.Increment(ref i);

                lock (lockTarget)
                {
                    var newPercentage = (double)Math.Round((100m / count) * i);

                    if (newPercentage != oldPercentage && newPercentage % 5 == 0)
                    {
                        Ux.WriteLine(string.Format("{0}% ", newPercentage));
                        oldPercentage = newPercentage;
                    }
                }
            });

            Ux.WriteLine(string.Format("Inline Inline Level CSS Instances: {0}", results.Sum(x => x.Page.Count)));
            Ux.WriteLine(string.Format("Inline Inline Level CSS Lines Of Code: {0}", results.Sum(x => x.Page.Sum(y => y))));
            Ux.WriteLine(string.Format("Total Inline Level CSS Instances: {0}", results.Sum(x => x.Inline.Count)));
            Ux.WriteLine(string.Format("Total Razor Level CSS Instances: {0}", results.Sum(x => x.Razor.Count)));

            return results;
        }
    }

    public interface ICssStatsPresenter : IPresenter
    {
        List<CssEvaluationResult> Present(List<string> files);
    }
}