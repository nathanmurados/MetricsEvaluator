using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MetricsEvaluationUtility.Services.Extensions;
using MetricsEvaluationUtility.ViewModels;

namespace MetricsEvaluationUtility.Services.Evaluators.JavaScript
{
    public class JsValidationEvaluator : IJsValidationEvaluator
    {
        public IJsRazorEvaluator JsRazorEvaluator { get; private set; }
        public IJsPageEvaluator JsPageEvaluator { get; private set; }
        public IJsBlockEvaluator JsBlockEvaluator { get; private set; }
        public IJsReferencesEvaluator JsReferencesEvaluator { get; private set; }

        public JsValidationEvaluator(IJsBlockEvaluator jsBlockEvaluator, IJsPageEvaluator jsPageEvaluator, IJsRazorEvaluator jsRazorEvaluator, IJsReferencesEvaluator jsReferencesEvaluator)
        {
            JsReferencesEvaluator = jsReferencesEvaluator;
            JsBlockEvaluator = jsBlockEvaluator;
            JsPageEvaluator = jsPageEvaluator;
            JsRazorEvaluator = jsRazorEvaluator;
        }


        public JavaScriptEvaluationResult Evaluate(string filename, string[] contents, List<string> attributes)
        {
            var joinedString = string.Join("", contents);

            if ((!joinedString.Contains("script", StringComparison.OrdinalIgnoreCase) && attributes.Any(x => joinedString.Contains(x, StringComparison.OrdinalIgnoreCase))))
            {
                return null;
            }

            var stopWatch = new Stopwatch();

            Debug.WriteLine("Filename: {0}", filename);
            Debug.WriteLine("Size: {0}", joinedString.Length);
            stopWatch.Start();
            var pageLevel = JsPageEvaluator.Evaluate(contents, joinedString);
            stopWatch.Stop();
            Debug.WriteLine("PageInstances: {1}r {0}t, {2}r/t", stopWatch.ElapsedTicks, pageLevel.Sum(x => x),(double) pageLevel.Sum(x => x) / stopWatch.ElapsedTicks);
            stopWatch.Reset();

            stopWatch.Start();
            var references = JsReferencesEvaluator.Evaluate(joinedString);
            stopWatch.Stop();
            Debug.WriteLine("References: {1}r {0}t, {2}r/t", stopWatch.ElapsedTicks, references, (double)references / stopWatch.ElapsedTicks);
            stopWatch.Reset();

            stopWatch.Start();
            var block = JsBlockEvaluator.Evaluate(joinedString, attributes);
            stopWatch.Stop();
            Debug.WriteLine("Inline: {1}r {0}t, {2}r/t", stopWatch.ElapsedTicks, block.Sum(x => x.InlineJavascriptTags.Count), (double)block.Sum(x => x.InlineJavascriptTags.Count) / stopWatch.ElapsedTicks);
            stopWatch.Reset();

            stopWatch.Start();
            var razor = JsRazorEvaluator.Evaluate(joinedString, attributes);
            stopWatch.Stop();
            Debug.WriteLine("Razor: {1}r {0}t, {2}r/t", stopWatch.ElapsedTicks, razor.Sum(x => x.InlineJavascriptTags.Count), (double)razor.Sum(x => x.InlineJavascriptTags.Count) / stopWatch.ElapsedTicks);
            stopWatch.Reset();

            Debug.WriteLine("---");
            Debug.WriteLine("");

            if (pageLevel.Any() || references > 0 || block.Any() || razor.Any())
            {
                return new JavaScriptEvaluationResult
                {
                    FileName = filename,
                    PageInstances = pageLevel,
                    References = references,
                    Block = block,
                    Razor = razor,
                };
            }

            return null;
        }
    }

    public interface IJsValidationEvaluator
    {
        JavaScriptEvaluationResult Evaluate(string filename, string[] contents, List<string> attributes);
    }
}