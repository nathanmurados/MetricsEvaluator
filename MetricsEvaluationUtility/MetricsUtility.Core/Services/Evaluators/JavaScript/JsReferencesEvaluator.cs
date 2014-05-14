using System;
using System.Text.RegularExpressions;
using MetricsUtility.Core.Services.Extensions;

namespace MetricsUtility.Core.Services.Evaluators.JavaScript
{
    public class JsReferencesEvaluator : IJsReferencesEvaluator
    {
        public int Evaluate(string content)
        {
            if (content.Contains("script", StringComparison.OrdinalIgnoreCase))
            {
                var openingRegex = new Regex("<script[^>]+src\\s*=\\s*['\"]([^'\"]+)['\"][^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase);

                var openingTagMatches = openingRegex.Matches(content);

                return openingTagMatches.Count;
            }
            return 0;
        }
    }
}