using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MetricsUtility.Core.Constants.Enums;
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
        public IJsInjectNewModuleVariables JsInjectNewModuleVariables { get; private set; }

        public AdvancedJsSeperationService(IJsBlockContentEvaluator jsBlockContentEvaluator, IJsFileNameEvaluator jsFileNameEvaluator, IJsModuleBlockEvaluator jsModuleBlockEvaluator, IJsModuleFactory jsModuleFactory, IJsInjectNewModuleVariables jsInjectNewModuleVariables)
        {
            JsInjectNewModuleVariables = jsInjectNewModuleVariables;
            JsModuleFactory = jsModuleFactory;
            JsModuleBlockEvaluator = jsModuleBlockEvaluator;
            JsFileNameEvaluator = jsFileNameEvaluator;
            JsBlockContentEvaluator = jsBlockContentEvaluator;
        }

        public SeperatedJs Evaluate(string[] lines, string solutionRouteDirectory, string generatedResultDirectory, string fileName, bool mergeBlocks)
        {
            var cleanedLines = new string[lines.Length];
            const string correct = "<script type=\"text/javascript\">";
            for (var i = 0; i < lines.Length; i++)
            {
                cleanedLines[i] = lines[i]
                    .Replace("<script>", correct)
                    .Replace("<script language=\"javascript\" type=\"text/javascript\">", correct);
            }

            var jsBlockContents = JsBlockContentEvaluator.Evaluate(cleanedLines, PageEvaluationMode.RazorOnly,mergeBlocks).ToList();

            GeneratedJsViewModel[] jsRemoved;
            List<string> refactoredLines;


            if (jsBlockContents.Any())
            {/*
                * find the first script reference
                * 
                * Convert it with the js module
                * 
                * Create new js files without razor and references to them
                * 
                * put in references to new Js files where old files WERE
                * 
                * Done?
                */



                // We may have several blocks of JS in the view. But we will have only one new AP2 module.
                // There may be duplicated razor fragments in the existing JS blocks. The duplication must be removed
                // before adding the fragments to the new ap2 module.

                jsRemoved = new GeneratedJsViewModel[jsBlockContents.Count];

                List<JsModuleViewModel> razorLines = new List<JsModuleViewModel>();
                for (var i = 0; i < jsBlockContents.Count; i++)
                {
                    var blockContent = jsBlockContents[i];
                    razorLines.AddRange(JsModuleBlockEvaluator.Evaluate(blockContent.Lines));

                    jsRemoved[i] = new GeneratedJsViewModel
                    {
                        Lines = JsInjectNewModuleVariables.Build(blockContent.Lines, razorLines).ToList()
                    };
                }


                //(LAST?) PROBLEM IS HERE!!
                var jsModule = JsModuleFactory.Build(razorLines.Distinct().ToList()); // generate the new ap2 module from the de-duplicated razor fragments

                refactoredLines = new List<string>();

                var jsFileDetails = new RefactoredFileNameViewModel[jsBlockContents.Count];
                var blockIndex = 0;
                var lineIndex = 0;
                var done = false;

                for (var i = 0; i < jsBlockContents.Count; i++)
                {
                    jsFileDetails[i] = JsFileNameEvaluator.Evaluate(solutionRouteDirectory, generatedResultDirectory, fileName, i);
                }

                var moduleHasBeenIncluded = false;

                foreach (var l in cleanedLines)
                {
                    if (!done)
                    {
                        var toReplace = jsBlockContents[blockIndex].Lines[lineIndex];

                        if (l.Contains(toReplace))
                        {
                            var hasStartTag = Regex.Matches(toReplace, RegexConstants.ScriptOpeningTag, RegexOptions.IgnoreCase).Count > 0;
                            var line = l;

                            if (hasStartTag)
                            {
                                if (!moduleHasBeenIncluded)
                                {
                                    refactoredLines.AddRange(jsModule);
                                    moduleHasBeenIncluded = true;
                                }

                                line = Regex.Replace(line, toReplace, jsFileDetails[blockIndex].HtmlLink, RegexOptions.IgnoreCase);

                            }
                            else
                            {
                                line = line.Remove(toReplace);
                            }

                            if (line.Trim().Length > 0)
                            {
                                refactoredLines.Add(line);
                            }

                            if (lineIndex == jsBlockContents[blockIndex].Lines.Count - 1)
                            {
                                jsRemoved[blockIndex].ProposedFileName = jsFileDetails[blockIndex].Filename;
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
                            refactoredLines.Add(l);
                        }
                    }
                    else
                    {
                        refactoredLines.Add(l);
                    }
                }
            }
            else
            {
                jsRemoved = new GeneratedJsViewModel[0];
                refactoredLines = cleanedLines.ToList();
            }

            return new SeperatedJs
            {
                JsRemoved = jsRemoved,
                RefactoredLines = refactoredLines.ToArray()
            };
        }
    }
}