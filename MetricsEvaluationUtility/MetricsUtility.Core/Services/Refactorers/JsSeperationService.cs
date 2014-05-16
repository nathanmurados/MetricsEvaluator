using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MetricsUtility.Core.Enums;
using MetricsUtility.Core.Services.Evaluators.JavaScript;

namespace MetricsUtility.Core.Services.Refactorers
{
    public class JsSeperationService : IJsSeperationService
    {
        public IJsBlockContentEvaluator JsBlockContentEvaluator { get; private set; }
        public IJsFileNameEvaluator JsFileNameEvaluator { get; private set; }

        public JsSeperationService(IJsBlockContentEvaluator jsBlockContentEvaluator, IJsFileNameEvaluator jsFileNameEvaluator)
        {
            JsFileNameEvaluator = jsFileNameEvaluator;
            JsBlockContentEvaluator = jsBlockContentEvaluator;
        }

        public SeperatedJs Evaluate(string[] lines, string solutionRouteDirectory, string generatedResultDirectory, string fileName)
        {
            var cleanedLines = new string[lines.Length];
            const string correct = "<script type=\"text/javascript\">";
            for (var i = 0; i < lines.Length; i++)
            {
                cleanedLines[i] = lines[i]
                    .Replace("<script>", correct)
                    .Replace("<script language=\"javascript\" type=\"text/javascript\">", correct);
            }

            var inlineJs = JsBlockContentEvaluator.Evaluate(cleanedLines, JsPageEvaluationMode.NonRazorOnly);

            GeneratedJsViewModel[] extractedJsBlocks;
            List<string> replacmentContent;


            if (!inlineJs.Any())
            {
                extractedJsBlocks = new GeneratedJsViewModel[0];
                replacmentContent = cleanedLines.ToList();
            }
            else
            {
                replacmentContent = new List<string>();
                extractedJsBlocks = new GeneratedJsViewModel[inlineJs.Count()];

                var jsFileDetails = new RefactoredFileNameViewModel[inlineJs.Count()];
                var blockIndex = 0;
                var lineIndex = 0;
                var done = false;

                for (var i = 0; i < inlineJs.Count(); i++)
                {
                    extractedJsBlocks[i] = new GeneratedJsViewModel { Lines = new List<string>() };
                    jsFileDetails[i] = JsFileNameEvaluator.Evaluate(solutionRouteDirectory, generatedResultDirectory, fileName, i);
                }

                var openingTagWrittenFor = -1;

                foreach (var l in cleanedLines)
                {
                    if (!done)
                    {
                        var toReplace = inlineJs[blockIndex].Lines[lineIndex];

                        if (l.Contains(toReplace))
                        {
                            var hasStartTag = Regex.Matches(toReplace, RegexConstants.ScriptOpeningTag, RegexOptions.IgnoreCase).Count > 0;
                            var line = l;

                            var cssReplacement = Regex.Replace(toReplace, RegexConstants.ScriptClosingTag, "", RegexOptions.IgnoreCase);

                            if (hasStartTag)
                            {
                                cssReplacement = Regex.Replace(cssReplacement, RegexConstants.ScriptOpeningTag, "", RegexOptions.IgnoreCase);
                                if (openingTagWrittenFor != blockIndex)
                                {
                                    line = Regex.Replace(line, toReplace, jsFileDetails[blockIndex].HtmlLink, RegexOptions.IgnoreCase);
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
                                replacmentContent.Add(line.Trim());
                            }

                            if (cssReplacement.Trim().Length > 0)
                            {
                                extractedJsBlocks[blockIndex].Lines.Add(cssReplacement);
                            }
                            if (lineIndex == inlineJs[blockIndex].Lines.Count - 1)
                            {
                                extractedJsBlocks[blockIndex].ProposedFileName = jsFileDetails[blockIndex].Filename;
                                lineIndex = -1;
                                blockIndex++;
                                if (blockIndex == inlineJs.Length)
                                {
                                    done = true;
                                }
                            }
                            lineIndex++;
                        }
                        else
                        {
                            replacmentContent.Add(l);
                        }
                    }
                    else
                    {
                        replacmentContent.Add(l);
                    }
                }
            }
            return new SeperatedJs
            {
                ExtractedJsBlocks = extractedJsBlocks,
                ReplacementLines = replacmentContent.ToArray()
            };
        }
    }
}