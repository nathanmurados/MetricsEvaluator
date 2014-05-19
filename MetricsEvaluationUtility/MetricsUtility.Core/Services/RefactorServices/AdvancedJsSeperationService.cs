using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MetricsUtility.Core.Enums;
using MetricsUtility.Core.Services.Evaluators.JavaScript;
using MetricsUtility.Core.ViewModels;

namespace MetricsUtility.Core.Services.RefactorServices
{
    public class AdvancedJsSeperationService : IAdvancedJsSeperationService
    {
        public IJsBlockContentEvaluator JsBlockContentEvaluator { get; private set; }
        public IJsFileNameEvaluator JsFileNameEvaluator { get; private set; }
        public IJsModuleBlockEvaluator JsModuleBlockEvaluator { get; private set; }
        public IJsModuleFactory JsModuleFactory { get; private set; }

        public AdvancedJsSeperationService(IJsBlockContentEvaluator jsBlockContentEvaluator, IJsFileNameEvaluator jsFileNameEvaluator, IJsModuleBlockEvaluator jsModuleBlockEvaluator, IJsModuleFactory jsModuleFactory)
        {
            JsModuleFactory = jsModuleFactory;
            JsModuleBlockEvaluator = jsModuleBlockEvaluator;
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

            var jsBlockContents = JsBlockContentEvaluator.Evaluate(cleanedLines, JsPageEvaluationMode.RazorOnly).ToList();

            GeneratedJsViewModel[] extractedJsBlocks;
            List<string> strippedContent;


            if (jsBlockContents.Any())
            {
                // We may have several blocks of JS in the view. But we will have only one new AP2 module.
                // There may be duplicated razor fragments in the existing JS blocks. The duplication must be removed
                // before adding the fragments to the new ap2 module.
                
                var jsModuleToInsert = new List<string[]>();

                List<JsModuleViewModel> totalRazorLines = new List<JsModuleViewModel>();
                
                foreach (var blockContent in jsBlockContents)
                {
                    IEnumerable<JsModuleViewModel> blockRazorLines = JsModuleBlockEvaluator.Evaluate(blockContent.Lines);
                    totalRazorLines.AddRange(blockRazorLines);
                }
                
                totalRazorLines = totalRazorLines.Distinct().ToList(); // de-duplicate razor fragments

                var jsModule = JsModuleFactory.Build(totalRazorLines); // generate the new ap2 module
             
                jsModuleToInsert.Add(jsModule);

                throw new NotImplementedException("NATHAN YOU ARE HERE");
                

                strippedContent = new List<string>();
                extractedJsBlocks = new GeneratedJsViewModel[jsBlockContents.Count];

                var jsFileDetails = new RefactoredFileNameViewModel[jsBlockContents.Count];
                var blockIndex = 0;
                var lineIndex = 0;
                var done = false;

                for (var i = 0; i < jsBlockContents.Count; i++)
                {
                    extractedJsBlocks[i] = new GeneratedJsViewModel { Lines = new List<string>() };
                    jsFileDetails[i] = JsFileNameEvaluator.Evaluate(solutionRouteDirectory, generatedResultDirectory, fileName, i);
                }

                var openingTagWrittenFor = -1;

                foreach (var l in cleanedLines)
                {
                    if (!done)
                    {
                        var toReplace = jsBlockContents[blockIndex].Lines[lineIndex];

                        if (l.Contains(toReplace))
                        {
                            var hasStartTag = Regex.Matches(toReplace, RegexConstants.ScriptOpeningTag, RegexOptions.IgnoreCase).Count > 0;
                            var line = l;

                            var replacement = Regex.Replace(toReplace, RegexConstants.ScriptClosingTag, "", RegexOptions.IgnoreCase);

                            if (hasStartTag)
                            {
                                replacement = Regex.Replace(replacement, RegexConstants.ScriptOpeningTag, "", RegexOptions.IgnoreCase);
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
                                strippedContent.Add(line.Trim());
                            }

                            if (replacement.Trim().Length > 0)
                            {
                                extractedJsBlocks[blockIndex].Lines.Add(replacement);
                            }

                            if (lineIndex == jsBlockContents[blockIndex].Lines.Count - 1)
                            {
                                extractedJsBlocks[blockIndex].ProposedFileName = jsFileDetails[blockIndex].Filename;
                                lineIndex = -1;
                                blockIndex++;
                                if (blockIndex == jsBlockContents.Count)
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
            else
            {
                extractedJsBlocks = new GeneratedJsViewModel[0];
                strippedContent = cleanedLines.ToList();
            }

            return new SeperatedJs
            {
                ExtractedJsBlocks = extractedJsBlocks,
                ReplacementLines = strippedContent.ToArray()
            };
        }
    }
}