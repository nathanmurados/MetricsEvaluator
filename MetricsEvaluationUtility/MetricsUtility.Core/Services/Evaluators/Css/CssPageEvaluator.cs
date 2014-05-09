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
        /// <param name="lines"></param>
        /// <returns></returns>
        public List<List<string>> Evaluate(IEnumerable<string> lines)
        {
            //var pageLevelCss = 0;
            var withinPageLevelCss = false;
            var matches = new List<List<string>>();
            const string closeTag = "</style>";

            var openingRegex = new Regex("<style[^>]+type\\s*=\\s*['\"]([^'\"]+)['\"][^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase);

            var ls = new List<string>();

            foreach (var line in lines)
            {
                var openingTagMatches = openingRegex.Matches(line);

                if (openingTagMatches.Count > 0)
                {
                    withinPageLevelCss = true;
                }
                if (withinPageLevelCss)
                {
                    if (line.Contains(closeTag))
                    {
                        var indexOfClosingTag = line.IndexOf(closeTag, StringComparison.InvariantCultureIgnoreCase);
                        ls.Add(line.Substring(0, indexOfClosingTag + closeTag.Length));
                    }
                    else
                    {
                        ls.Add(line.Trim());
                    }
                }

                if (line.Contains(closeTag, StringComparison.OrdinalIgnoreCase))
                {
                    withinPageLevelCss = false;
                    matches.Add(ls);
                    ls = new List<string>();
                }
            }

            return matches;
        }
    }
}