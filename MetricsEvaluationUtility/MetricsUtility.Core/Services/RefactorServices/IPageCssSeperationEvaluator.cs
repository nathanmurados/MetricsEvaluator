using MetricsUtility.Core.ViewModels;

namespace MetricsUtility.Core.Services.RefactorServices
{
    public interface IPageCssSeperationEvaluator
    {
        SeperatedCssViewModel Evaluate(string[] lines, string solutionRouteDirectory, string directoryForGeneratedCss, string fileName);
    }
}