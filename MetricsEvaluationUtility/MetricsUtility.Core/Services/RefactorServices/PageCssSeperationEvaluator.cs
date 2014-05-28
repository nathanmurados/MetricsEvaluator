using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MetricsUtility.Core.Constants.Enums;
using MetricsUtility.Core.Services.Evaluators.Css;
using MetricsUtility.Core.ViewModels;

namespace MetricsUtility.Core.Services.RefactorServices
{
    public class PageCssSeperationEvaluator : IPageCssSeperationEvaluator
    {
        public ICssBlockContentEvaluator CssBlockContentEvaluator { get; private set; }
        public ICssFileNameEvaluator CssFileNameEvaluator { get; private set; }

        public PageCssSeperationEvaluator(ICssBlockContentEvaluator cssBlockContentEvaluator, ICssFileNameEvaluator cssFileNameEvaluator)
        {
            CssFileNameEvaluator = cssFileNameEvaluator;
            CssBlockContentEvaluator = cssBlockContentEvaluator;
        }

        public SeperatedCssViewModel Evaluate(string[] lines, string solutionRouteDirectory, string directoryForGeneratedCss, string fileName, bool mergeBlocks)
        {
            var cleanedLines = new string[lines.Count()];
            for (int i = 0; i < lines.Length; i++)
            {
                cleanedLines[i] = lines[i].Replace("<style>", "<style type=\"text/css\">");
            }

            var inlineCss = CssBlockContentEvaluator.Split(cleanedLines, PageEvaluationMode.NonRazorOnly,mergeBlocks);

            GeneratedCssViewModel[] extractedCssBlocks;
            List<string> strippedContent;


            if (!inlineCss.Any())
            {
                extractedCssBlocks = new GeneratedCssViewModel[0];
                strippedContent = cleanedLines.ToList();
            }
            else
            {
                strippedContent = new List<string>();
                extractedCssBlocks = new GeneratedCssViewModel[inlineCss.Length];

                var cssFileDetails = new RefactoredFileNameViewModel[inlineCss.Length];
                var blockIndex = 0;
                var lineIndex = 0;
                var done = false;

                for (var i = 0; i < inlineCss.Length; i++)
                {
                    extractedCssBlocks[i] = new GeneratedCssViewModel { Lines = new List<string>() };
                    cssFileDetails[i] = CssFileNameEvaluator.Evaluate(solutionRouteDirectory, directoryForGeneratedCss, fileName, i);
                }

                var openingTagWrittenFor = -1;

                foreach (var l in cleanedLines)
                {
                    if (!done)
                    {
                        var toReplace = inlineCss[blockIndex].Lines[lineIndex];

                        if (l.Contains(toReplace))
                        {
                            var hasStartTag = Regex.Matches(toReplace, RegexConstants.StyleOpeningTag, RegexOptions.IgnoreCase).Count > 0;
                            var line = l;

                            var cssReplacement = Regex.Replace(toReplace, "</style>", "", RegexOptions.IgnoreCase);

                            if (hasStartTag)
                            {
                                cssReplacement = Regex.Replace(cssReplacement, RegexConstants.StyleOpeningTag, "", RegexOptions.IgnoreCase);
                                if (openingTagWrittenFor != blockIndex)
                                {
                                    line = Regex.Replace(line, toReplace, cssFileDetails[blockIndex].HtmlLink, RegexOptions.IgnoreCase);
                                    openingTagWrittenFor = blockIndex;
                                }
                                else
                                {
                                    line = Regex.Replace(line, toReplace, "", RegexOptions.IgnoreCase);
                                }
                            }
                            else
                            {
                                line = line.Remove(toReplace);
                            }

                            if (line.Trim().Length > 0)
                            {
                                strippedContent.Add(line.Trim());
                            }

                            if (cssReplacement.Trim().Length > 0)
                            {
                                extractedCssBlocks[blockIndex].Lines.Add(cssReplacement.Trim());
                            }
                            if (lineIndex == inlineCss[blockIndex].Lines.Count - 1)
                            {
                                extractedCssBlocks[blockIndex].ProposedFileName = cssFileDetails[blockIndex].Filename;
                                lineIndex = -1;
                                blockIndex++;
                                if (blockIndex == inlineCss.Length)
                                {
                                    done = true;
                                }
                            }
                            lineIndex++;
                        }
                        else
                        {
                            strippedContent.Add(l);
                        }
                    }
                    else
                    {
                        strippedContent.Add(l);
                    }
                }
            }
            return new SeperatedCssViewModel
            {
                ExtractedCssBlocks = extractedCssBlocks,
                StripedContent = strippedContent.ToArray()
            };
        }
    }
}
