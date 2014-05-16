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
        public static object LockTarget = new object();


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
            double newPercentage;

            var attributes = JsAttributesProvider.Attributes.ToList();

            if (true)
            {
                Parallel.ForEach(files, file =>
                {
                    var contents = File.ReadAllLines(file);

                    var result = JsValidationEvaluator.Evaluate(file, contents, attributes);
                    if (result != null)
                    {
                        results.Add(result);
                    }

                    Interlocked.Increment(ref i);

                    lock (LockTarget)
                    {
                        newPercentage = (double)Math.Round((100m / count) * i);

                        if (newPercentage != oldPercentage /* && newPercentage % 5 == 0*/)
                        {
                            Ux.UpdateProgress((int)newPercentage);
                            oldPercentage = newPercentage;
                        }
                    }
                });
            }
            else
            {
                foreach (var result in from file in files let contents = File.ReadAllLines(file) select JsValidationEvaluator.Evaluate(file, contents, attributes))
                {
                    if (result != null)
                    {
                        results.Add(result);
                    }

                    i++;
                    newPercentage = (double)Math.Round((100m / count) * i);

                    if (newPercentage != oldPercentage /* && newPercentage % 5 == 0*/)
                    {
                        Ux.UpdateProgress((int)newPercentage);
                        oldPercentage = newPercentage;
                    }
                }
            }

            var comparer = StringComparer.OrdinalIgnoreCase;

            Ux.WriteLine(string.Format("Total number of files: {0}", results.Count()));
            Ux.WriteLine(string.Format("Total <script type='text/javascript'> declarations: {0}", results.Sum(x => x.PageInstances.Length)));
            Ux.WriteLine(string.Format("Total <script type='text/javascript'> lines of code: {0}", results.Sum(x => x.PageInstances.Sum(y => y.Lines.Count))));
            Ux.WriteLine(string.Format("Total <script type='text/javascript'> lines of code containing '@': {0}", results.Sum(x => x.PageInstances.Sum(y => y.AtSymbols))));
            Ux.WriteLine(string.Format("Total <div onSomeEvent='...'> declarations: {0}", results.Sum(x => x.Block.Sum(y => y.InlineJavascriptTags.Count))));

            foreach (var attr in attributes.Where(tag => results.Any(x => x.Block.Any(y => comparer.Equals(y.AttributeName, tag)))))
            {
                Ux.WriteLine(string.Format("Total html {0} instances: {1}", attr, results.Sum(x => x.Block.Count(y => comparer.Equals(y.AttributeName, attr)))));
            }

            Ux.WriteLine(string.Format("Total Razor declarations: {0}", results.Sum(x => x.Razor.Count)));
            foreach (var attr in attributes.Where(tag => results.Any(x => x.Razor.Any(y => comparer.Equals(y.AttributeName, tag)))))
            {
                Ux.WriteLine(string.Format("Total razor {0} instances: {1}", attr, results.Sum(x => x.Razor.Count(y => comparer.Equals(y.AttributeName, attr)))));
            }

            Ux.ResetProgress();

            return results;
        }
    }

    public interface IJavaScriptStatsPresenter
    {
        List<JavaScriptEvaluationResult> Present(List<string> files);
    }
}