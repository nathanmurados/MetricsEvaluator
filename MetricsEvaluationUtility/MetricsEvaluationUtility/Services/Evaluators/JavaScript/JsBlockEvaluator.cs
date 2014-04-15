using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MetricsEvaluationUtility.Services.Extensions;
using MetricsEvaluationUtility.ViewModels;

namespace MetricsEvaluationUtility.Services.Evaluators.JavaScript
{
    public class JsBlockEvaluator : IJsBlockEvaluator
    {
        public List<DetailedJavaScriptEvaluationResult> Evaluate(string content, IEnumerable<string> attributes)
        {
            var finalResults = new List<DetailedJavaScriptEvaluationResult>();

            foreach (var tag in attributes)
            {
                if (content.Contains(tag, StringComparison.OrdinalIgnoreCase))
                {
                    var regex = new Regex(string.Format("<[^>]+\\s*{0}\\s*=\\s*['\"]([^'\"]+)['\"][^>]*>", tag), RegexOptions.Multiline | RegexOptions.IgnoreCase);
                    var matches = regex.Matches(content);

                    if (matches.Count > 0)
                    {
                        var result = new DetailedJavaScriptEvaluationResult
                        {
                            AttributeName = tag,
                            InlineJavascriptTags = new List<JavascriptOccurenceResult>()
                        };

                        foreach (Match match in matches)
                        {
                            result.InlineJavascriptTags.Add(new JavascriptOccurenceResult { Value = match.Value });
                        }

                        finalResults.Add(result);
                    }
                }
            }

            return finalResults;
        }
    }

    public interface IJsBlockEvaluator
    {
        List<DetailedJavaScriptEvaluationResult> Evaluate(string content, IEnumerable<string> attributes);
    }
}