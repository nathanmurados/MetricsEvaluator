using System;
using System.Collections.Generic;
using System.Linq;
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
        public List<CssPageEvaluationResult> Evaluate(string[] lines)
        {
            //var pageLevelCss = 0;
            var withinPageLevelCss = false;
            var matches = new List<CssPageEvaluationResult>();
            const string closeTag = "</style>";

            var openingRegex = new Regex("<style[^>]+type\\s*=\\s*['\"]([^'\"]+)['\"][^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase);

            var ls = new CssPageEvaluationResult { Lines = new List<string>() };

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];

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
                        ls.Lines.Add(line.Substring(0, indexOfClosingTag + closeTag.Length));
                        if (ls.FirstOccurenceLineNumber == 0)
                        {
                            ls.FirstOccurenceLineNumber = i;
                        }
                    }
                    else
                    {
                        ls.Lines.Add(line.Trim());
                    }
                }

                if (line.Contains(closeTag, StringComparison.OrdinalIgnoreCase))
                {
                    withinPageLevelCss = false;
                    matches.Add(ls);
                    ls = new CssPageEvaluationResult { Lines = new List<string>() };
                }
            }

            return matches;
        }
    }

    public class CssPageEvaluationResult
    {
        public List<string> Lines { get; set; }
        public int FirstOccurenceLineNumber { get; set; }
    }
}