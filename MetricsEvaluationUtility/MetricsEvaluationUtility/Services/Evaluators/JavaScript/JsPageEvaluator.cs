using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MetricsEvaluationUtility.Services.Extensions;

namespace MetricsEvaluationUtility.Services.Evaluators.JavaScript
{
    public class JsPageEvaluator : IJsPageEvaluator
    {
        /// <summary>
        /// Derived from http://stackoverflow.com/questions/17200485/regex-to-match-a-html-tags-without-specific-attribute
        /// </summary>
        /// <param name="contents"></param>
        /// <returns></returns>
        public List<int> Evaluate(IEnumerable<string> contents, string joinedContent)
        {
            var matches = new List<int>();

            if (joinedContent.Contains("script", StringComparison.OrdinalIgnoreCase))
            {
                var pageLevelCss = 0;
                var withinPageLevelCss = false;

                var openingRegex = new Regex("<script(?=\\s|>)(?!(?:[^>=]|=(['\"])(?:(?!\\1).)*\\1)*?\\ssrc=['\"])[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase);

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

                    if (withinPageLevelCss && content.Contains("</script>", StringComparison.OrdinalIgnoreCase))
                    {
                        withinPageLevelCss = false;
                        matches.Add(pageLevelCss);
                        pageLevelCss = 0;
                    }
                }
            }

            return matches;
        }

    }

    public interface IJsPageEvaluator
    {
        List<int> Evaluate(IEnumerable<string> contents, string joinedContent);
    }
}