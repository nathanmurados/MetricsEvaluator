using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MetricsUtility.Core.Enums;
using MetricsUtility.Core.Services.Evaluators.Css;
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
        public IJsRazorRemovalService JsRazorRemovalService { get; private set; }

        public AdvancedJsSeperationService(IJsBlockContentEvaluator jsBlockContentEvaluator, IJsFileNameEvaluator jsFileNameEvaluator, IJsModuleBlockEvaluator jsModuleBlockEvaluator, IJsModuleFactory jsModuleFactory, IJsRazorRemovalService jsRazorRemovalService)
        {
            JsRazorRemovalService = jsRazorRemovalService;
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

            GeneratedJsViewModel[] jsRemoved;
            List<string> refactoredLines;


            if (jsBlockContents.Any())
            {
                // Mike's work - Start
                // We may have several blocks of JS in the view. But we can have only one new AP2 module (the ap2 variable would be repeated)
                // Therefore we must process all JS blocks, de-duplicated any razor variables between the blocks, and then generate the new ap2 module.

                List<JsModuleViewModel> razorLines = new List<JsModuleViewModel>();

                var cleanedJs = new List<string[]>();

                foreach (var jsToClean in jsBlockContents)
                {
                    razorLines.AddRange(JsModuleBlockEvaluator.Evaluate(jsToClean.Lines));

                    cleanedJs.Add(JsRazorRemovalService.Remove(jsToClean));
                }

                razorLines = razorLines.Distinct().ToList(); // de-duplicate

                var jsModule = JsModuleFactory.Build(razorLines); // the new sp2.xyz = @xyz stuff that's injected into the view.
                //Mike's work - End

                /*
                 * find the first script reference
                 * 
                 * Replace it with the js module
                 * 
                 * Create new js files without razor and references to them
                 * 
                 * put in references to new Js files where old files WERE
                 * 
                 * Done?
                 */

                refactoredLines = new List<string>();
                jsRemoved = new GeneratedJsViewModel[jsBlockContents.Count];

                var jsFileDetails = new RefactoredFileNameViewModel[jsBlockContents.Count];
                var blockIndex = 0;
                var lineIndex = 0;
                var done = false;

                for (var i = 0; i < jsBlockContents.Count; i++)
                {
                    jsRemoved[i] = new GeneratedJsViewModel { Lines = new List<string>() };
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
                                //if (openingTagWrittenFor != blockIndex)
                                //{
                                //    line = Regex.Replace(line, toReplace, jsFileDetails[blockIndex].HtmlLink, RegexOptions.IgnoreCase);
                                //    openingTagWrittenFor = blockIndex;
                                //}
                                //else
                                //{
                                //    line = Regex.Replace(line, toReplace, "", RegexOptions.IgnoreCase);
                                //}

                                refactoredLines.Add("<script type='text/javascript'>");
                                refactoredLines.AddRange(jsModule);
                                refactoredLines.Add("</script>");
                                
                                line = Regex.Replace(line, toReplace, jsFileDetails[blockIndex].HtmlLink, RegexOptions.IgnoreCase);

                            }
                            else
                            {
                                line = line.Remove(toReplace);
                            }

                            if (line.Trim().Length > 0)
                            {
                                refactoredLines.Add(line.Trim());
                            }

                            if (replacement.Trim().Length > 0)
                            {
                                jsRemoved[blockIndex].Lines.Add(replacement);
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

    //THIS IS GOING TO BE REPLACED BY MIKES CODE
    public interface IJsRazorRemovalService
    {
        string[] Remove(BlockContent jsToClean);
    }
    public class JsRazorRemovalService : IJsRazorRemovalService
    {
        public string[] Remove(BlockContent jsToClean)
        {
            throw new NotImplementedException();
        }
    }
}