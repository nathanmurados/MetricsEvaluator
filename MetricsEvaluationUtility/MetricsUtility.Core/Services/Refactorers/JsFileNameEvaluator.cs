using System.IO;
using System.Linq;

namespace MetricsUtility.Core.Services.Refactorers
{
    public class JsFileNameEvaluator : IJsFileNameEvaluator
    {
        public ISolutionRelativeDirectoryEvaluator SolutionRelativeDirectoryEvaluator { get; private set; }
        public JsFileNameEvaluator(ISolutionRelativeDirectoryEvaluator solutionRelativeDirectoryEvaluator)
        {
            SolutionRelativeDirectoryEvaluator = solutionRelativeDirectoryEvaluator;
        }

        public RefactoredFileNameViewModel Evaluate(string solutionDirectory, string newDirectory, string originalFileName, int fragment)
        {
            var fragmentStr = fragment > 0 ? "_fragment" + fragment : "";

            var fparts = originalFileName.Split('\\');
            var parts = fparts[fparts.Length - 1].Split('.');

            var generatedFileNameWithoutExtension = string.Join(".", parts.Take(parts.Length - 1));

            var generatedFileName = string.Format("{0}{1}.js", generatedFileNameWithoutExtension, fragmentStr);
            var i = 2;
            while (File.Exists(newDirectory + "\\" + generatedFileName))
            {
                generatedFileName = string.Format("{0}{1}({2}).js", generatedFileNameWithoutExtension, fragmentStr, i);
                i++;
            }


            var relDir = SolutionRelativeDirectoryEvaluator.Evaluate(solutionDirectory, newDirectory);

            return new RefactoredFileNameViewModel
            {
                Filename = generatedFileName,
                HtmlLink = "<script src=\"" + relDir + "/" + generatedFileName + "\" type=\"text/javascript\"></script>"
            };
        }
    }
}