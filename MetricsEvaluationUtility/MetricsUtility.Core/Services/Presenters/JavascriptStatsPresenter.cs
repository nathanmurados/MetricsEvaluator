using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MetricsUtility.Core.Services.Evaluators.JavaScript;
using MetricsUtility.Core.ViewModels;

namespace MetricsUtility.Core.Services.Presenters
{
    public class JavaScriptStatsPresenter : IJavaScriptStatsPresenter
    {
        public IHumanInterface Ux { get; private set; }
        public IJsValidationEvaluator JsValidationEvaluator { get; private set; }

        public JavaScriptStatsPresenter(IHumanInterface ux, IJsValidationEvaluator jsValidationEvaluator)
        {
            JsValidationEvaluator = jsValidationEvaluator;
            Ux = ux;
        }

        public List<JavaScriptEvaluationResult> Present(List<string> files)
        {
            var results = new List<JavaScriptEvaluationResult>();

            var count = files.Count();
            var i = 0;
            double oldPercentage = 0;

            var lockTarget = new object();

            var attributes = JsAttributesProvider.Attributes.ToList();

            Parallel.ForEach(files, file =>
            {
                var contents = File.ReadAllLines(file);

                var result = JsValidationEvaluator.Evaluate(file, contents, attributes);
                if (result != null)
                {
                    results.Add(result);
                }

                Interlocked.Increment(ref i);

                lock (lockTarget)
                {
                    var newPercentage = (double)Math.Round((100m / count) * i);

                    if (newPercentage != oldPercentage/* && newPercentage % 5 == 0*/)
                    {
                        Ux.UpdateProgress((int)newPercentage);
                        oldPercentage = newPercentage;
                    }
                }
            });


            var comparer = StringComparer.OrdinalIgnoreCase;

            Ux.WriteLine(string.Format("Inline Page Instances: {0}", results.Sum(x => x.PageInstances.Count)));
            Ux.WriteLine(string.Format("Inline Lines Of Code: {0}", results.Sum(x => x.PageInstances.Sum(y => y))));
            Ux.WriteLine(string.Format("Inline Block Instances: {0}", results.Sum(x => x.Block.Sum(y => y.InlineJavascriptTags.Count))));
            Ux.WriteLine(string.Format("Razor Instances: {0}", results.Sum(x => x.Razor.Count)));

            foreach (var tag in attributes)
            {
                if (results.Any(x =>
                        x.Block.Any(y => comparer.Equals(y.AttributeName, tag))
                        ||
                        x.Razor.Any(y => comparer.Equals(y.AttributeName, tag))
                        )
                    )
                {
                    Ux.WriteLine(string.Format("Total Inline/Razor {0} Instances: {1}", tag, results.Sum(x =>
                        x.Block.Count(y => comparer.Equals(y.AttributeName, tag))
                        + x.Razor.Count(y => comparer.Equals(y.AttributeName, tag)))));
                }
            }

            return results;
        }
    }

    public interface IJavaScriptStatsPresenter
    {
        List<JavaScriptEvaluationResult> Present(List<string> files);
    }
}