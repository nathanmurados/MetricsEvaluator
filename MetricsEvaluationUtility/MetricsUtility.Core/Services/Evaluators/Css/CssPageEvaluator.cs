using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MetricsUtility.Core.Services.Extensions;

namespace MetricsUtility.Core.Services.Evaluators.Css
{
    public class CssPageEvaluator : ICssPageEvaluator
    {
        /// <summary>
        /// Derived from http://stackoverflow.com/questions/1079423/regular-expression-to-get-an-attribute-from-html-tag
        /// </summary>
        /// <param name="contents"></param>
        /// <returns></returns>
        public List<int> Evaluate(IEnumerable<string> contents)
        {
            var pageLevelCss = 0;
            var withinPageLevelCss = false;
            var matches = new List<int>();

            var openingRegex = new Regex("<style[^>]+type\\s*=\\s*['\"]([^'\"]+)['\"][^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase);

            foreach (var content in contents)
            {
                var openingTagMatches = openingRegex.Matches(content);

                if (openingTagMatches.Count > 0)
                {
                    withinPageLevelCss = true;
                }
                if (withinPageLevelCss)
                {
                    pageLevelCss++;
                }

                if (content.Contains("</style>", StringComparison.OrdinalIgnoreCase))
                {
                    withinPageLevelCss = false;
                    matches.Add(pageLevelCss);
                    pageLevelCss = 0;
                }
            }

            return matches;
        }
    }

    public interface ICssPageEvaluator
    {
        List<int> Evaluate(IEnumerable<string> contents);
    }
}