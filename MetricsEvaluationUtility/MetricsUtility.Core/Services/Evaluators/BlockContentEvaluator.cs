using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MetricsUtility.Core.Constants.Enums;
using MetricsUtility.Core.Services.Evaluators.Css;
using MetricsUtility.Core.Services.Extensions;

namespace MetricsUtility.Core.Services.Evaluators
{
    public abstract class BlockContentEvaluator
    {
        public IRemediatedBlockJsRemover RemediatedBlockJsRemover { get; set; }

        public BlockContent[] Split(string[] lines, string openingRegexTag, string closeTag, PageEvaluationMode mode, bool mergeBlocks)
        {
            //var pageLevelCss = 0;
            var within = false;
            var matches = new List<BlockContent>();
            var isWhiteSpaceSinceLastBlock = false;
            var openingRegex = new Regex(openingRegexTag, RegexOptions.Multiline | RegexOptions.IgnoreCase);
            var atSymbols = 0;

            var ls = new BlockContent { Lines = new List<string>() };

            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i];

                var openingTagMatches = openingRegex.Matches(line);

                if (openingTagMatches.Count > 0)
                {
                    within = true;

                    var isMatch = openingTagMatches.Cast<Match>().Any(match => string.IsNullOrWhiteSpace(line.Replace(match.Value, "")));

                    isWhiteSpaceSinceLastBlock = isWhiteSpaceSinceLastBlock && (string.IsNullOrWhiteSpace(line) || isMatch);
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
                    isWhiteSpaceSinceLastBlock = isWhiteSpaceSinceLastBlock && string.IsNullOrWhiteSpace(line);                    
                }

                if (within && line.Contains(closeTag, StringComparison.OrdinalIgnoreCase))
                {
                    within = false;
                    if (
                            (mode==PageEvaluationMode.RazorOnly && atSymbols > 0)
                        ||  (mode==PageEvaluationMode.NonRazorOnly && atSymbols == 0)
                        ||  (mode==PageEvaluationMode.Any)
                    )
                    {
                        if (mergeBlocks && isWhiteSpaceSinceLastBlock && matches.Any())
                        {
                            matches.Last().Lines.AddRange(ls.Lines);
                            matches.Last().AtSymbols += atSymbols;
                        }
                        else
                        {
                            ls.AtSymbols = atSymbols;
                            matches.Add(ls);
                            isWhiteSpaceSinceLastBlock = true;
                        }
                    }

                    ls = new BlockContent
                    {
                        Lines = new List<string>(),
                        AtSymbols = 0
                    };

                    atSymbols = 0;
                }
            }

            if (mode == PageEvaluationMode.RazorOnly && openingRegexTag == RegexConstants.ScriptOpeningTag)
            {
                matches = RemediatedBlockJsRemover.Remove(matches);
            }
            
            return matches.ToArray();
        }
        
    }

    //public static class JsPageEvaluationModeExtensions
    //{
    //    public static bool AllowAtVars(this PageEvaluationMode mode)
    //    {
    //        return mode != PageEvaluationMode.NonRazorOnly;
    //    }
    //}
}