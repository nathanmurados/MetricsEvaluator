using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MetricsUtility.Core.Enums;
using MetricsUtility.Core.Services.Extensions;

namespace MetricsUtility.Core.Services.Evaluators.Css
{
    public abstract class PageBlockSplitter
    {
        public List<PageBlockSplitResult> Split(string[] lines, string openingRegexTag, string closeTag, JsPageEvaluationMode mode)
        {
            //var pageLevelCss = 0;
            var within = false;
            var matches = new List<PageBlockSplitResult>();
            var isWhitSpaceSinceLastBlock = false;
            var openingRegex = new Regex(openingRegexTag, RegexOptions.Multiline | RegexOptions.IgnoreCase);
            var atSymbols = 0;

            var ls = new PageBlockSplitResult { Lines = new List<string>() };

            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i];

                var openingTagMatches = openingRegex.Matches(line);

                if (openingTagMatches.Count > 0)
                {
                    within = true;

                    var isMatch = openingTagMatches.Cast<Match>().Any(match => string.IsNullOrWhiteSpace(line.Replace(match.Value, "")));

                    isWhitSpaceSinceLastBlock = isWhitSpaceSinceLastBlock && (string.IsNullOrWhiteSpace(line) || isMatch);
                }

                if (within)
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
                        ls.Lines.Add(line);
                        atSymbols += line.Count(x => x == '@');
                    }
                }
                else
                {
                    isWhitSpaceSinceLastBlock = isWhitSpaceSinceLastBlock && string.IsNullOrWhiteSpace(line);                    
                }

                if (within && line.Contains(closeTag, StringComparison.OrdinalIgnoreCase))
                {
                    within = false;
                    if (
                            (mode==JsPageEvaluationMode.RazorOnly && atSymbols > 0)
                        ||  (mode==JsPageEvaluationMode.NonRazorOnly && atSymbols == 0)
                        ||  (mode==JsPageEvaluationMode.Any)
                    )
                    {
                        if (isWhitSpaceSinceLastBlock && matches.Any())
                        {
                            matches.Last().Lines.AddRange(ls.Lines);
                            matches.Last().AtSymbols += atSymbols;
                        }
                        else
                        {
                            ls.AtSymbols = atSymbols;
                            matches.Add(ls);
                            isWhitSpaceSinceLastBlock = true;
                        }
                    }

                    ls = new PageBlockSplitResult
                    {
                        Lines = new List<string>(),
                        AtSymbols = 0
                    };
                }
            }

            return matches;
        }
    }

    //public static class JsPageEvaluationModeExtensions
    //{
    //    public static bool AllowAtVars(this JsPageEvaluationMode mode)
    //    {
    //        return mode != JsPageEvaluationMode.NonRazorOnly;
    //    }
    //}
}