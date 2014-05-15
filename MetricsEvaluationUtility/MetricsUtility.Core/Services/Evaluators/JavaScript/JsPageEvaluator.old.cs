using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MetricsUtility.Core.Services.Extensions;

namespace MetricsUtility.Core.Services.Evaluators.JavaScript
{
    //public class JsPageEvaluator : IJsPageEvaluator
    //{
    //    /// <summary>
    //    /// Derived from http://stackoverflow.com/questions/17200485/regex-to-match-a-html-tags-without-specific-attribute
    //    /// </summary>
    //    /// <param name="contents"></param>
    //    /// <returns></returns>
    //    public List<JsPageEvaluationResult> Evaluate(List<string> contents)
    //    {
    //        var matches = new List<JsPageEvaluationResult>();

    //        if (contents.Any(x => x.Contains("script", StringComparison.OrdinalIgnoreCase)))
    //        {
    //            var lines = new List<string>();
    //            var within = false;
    //            var atSymbols = 0;
    //            var openingRegex = new Regex(RegexConstants.OpeningTag, RegexOptions.Multiline | RegexOptions.IgnoreCase);

    //            foreach (var content in contents)
    //            {
    //                var openingTagMatches = openingRegex.Matches(content);

    //                if (openingTagMatches.Count > 0)
    //                {
    //                    within = true;
    //                }
    //                if (within)
    //                {
    //                    lines.Add(content);
    //                    atSymbols += content.Count(x => x == '@');
    //                }

    //                if (within && content.Contains("</script>", StringComparison.OrdinalIgnoreCase))
    //                {
    //                    within = false;
    //                    matches.Add(new JsPageEvaluationResult
    //                    {
    //                        AtSymbolOccurences = atSymbols,
    //                        Lines = lines
    //                    });
    //                    lines = new List<string>();
    //                    atSymbols = 0;
    //                }
    //            }
    //        }

    //        return matches;
    //    }
    //}

    //public class JsPageEvaluationResult
    //{
    //    public List<string> Lines { get; set; }
    //    public int AtSymbolOccurences { get; set; }
    //}
}