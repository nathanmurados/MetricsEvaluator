using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MetricsUtility.Core.Services.Evaluators.Css;
using MetricsUtility.Core.Services.StorageServices;
using MetricsUtility.Core.ViewModels;

namespace MetricsUtility.Core.Services.Presenters
{
    public class CssStatsPresenter : ICssStatsPresenter
    {
        public IHumanInterface Ux { get; private set; }
        public ICssValidationEvaluator CssValidationEvaluator { get; private set; }
        public ICssStatsStorageService CssStatsStorageService { get; private set; }
        public static object Lock = new object();

        public CssStatsPresenter(IHumanInterface ux, ICssValidationEvaluator cssValidationEvaluator, ICssStatsStorageService cssStatsStorageService)
        {
            CssStatsStorageService = cssStatsStorageService;
            CssValidationEvaluator = cssValidationEvaluator;
            Ux = ux;
        }

        public List<CssEvaluationResult> Present(List<string> files)
        {
            var results = new List<CssEvaluationResult>();

            var i = 0;
            var count = files.Count();
            double oldPercentage = 0;


            Parallel.ForEach(files, file =>
            {
                var contents = File.ReadAllLines(file);

                var result = CssValidationEvaluator.Evaluate(file, contents, false);
                if (result != null)
                {
                    results.Add(result);
                }

                Interlocked.Increment(ref i);

                lock (Lock)
                {
                    var newPercentage = (double)Math.Round((100m / count) * i);

                    if (newPercentage != oldPercentage /* && newPercentage % 5 == 0*/)
                    {
                        Ux.UpdateProgress((int)newPercentage);
                        oldPercentage = newPercentage;
                    }
                }
            });

            Ux.WriteLine(string.Format("Total number of files: {0}", results.Count()));
            Ux.WriteLine(string.Format("<style type='text/css'> declarations: {0}", results.Sum(x => x.Page.Count)));
            Ux.WriteLine(string.Format("<style type='text/css'> lines of code: {0}", results.Sum(x => x.Page.Sum(y => y))));
            Ux.WriteLine(string.Format("<div style='...'> declarations: {0}", results.Sum(x => x.Inline.Count)));
            Ux.WriteLine(string.Format("Razor declarations: {0}", results.Sum(x => x.Razor.Count)));
            Ux.ResetProgress();

            return results;
        }
    }

    public interface ICssStatsPresenter : IPresenter
    {
        List<CssEvaluationResult> Present(List<string> files);
    }
}