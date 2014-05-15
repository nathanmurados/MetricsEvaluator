using System.Linq;

namespace MetricsUtility.Core.Services.Refactorers
{
    public class CssFileNameEvaluator : ICssFileNameEvaluator
    {
        public ISolutionRelativeDirectoryEvaluator SolutionRelativeDirectoryEvaluator { get; private set; }

        public CssFileNameEvaluator(ISolutionRelativeDirectoryEvaluator solutionRelativeDirectoryEvaluator)
        {
            SolutionRelativeDirectoryEvaluator = solutionRelativeDirectoryEvaluator;
        }

        public RefactoredFileNameViewModel Evaluate(string solutionDirectory, string newDirectory, string fileName, int fragment)
        {
            var fragmentStr = fragment > 0 ? "_fragment" + fragment : "";

            var fparts = fileName.Split('\\');
            var parts = fparts[fparts.Length - 1].Split('.');

            var newCssFileName = string.Format("{0}{1}.css", string.Join(".", parts.Take(parts.Length - 1)), fragmentStr);

            var relDir = SolutionRelativeDirectoryEvaluator.Evaluate(solutionDirectory, newDirectory);

            return new RefactoredFileNameViewModel
            {
                Filename = newCssFileName,
                HtmlLink = "<link href=\"" + relDir + "/" + newCssFileName + "\" rel=\"stylesheet\" />"
            };
        }
    }
}