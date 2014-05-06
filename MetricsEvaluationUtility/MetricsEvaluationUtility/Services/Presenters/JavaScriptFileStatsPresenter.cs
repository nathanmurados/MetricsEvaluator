using System.Collections.Generic;
using System.IO;
using MetricsEvaluationUtility.Services.Evaluators;

namespace MetricsEvaluationUtility.Services.Presenters
{
    public class JavaScriptFileStatsPresenter : IJavaScriptFileStatsPresenter
    {
        public IHumanInterface Ux { get; private set; }
        public IFilteredFilesEvaluator FilteredFilesEvaluator { get; private set; }
        public IJavaScriptStatsPresenter JavaScriptStatsPresenter { get; private set; }

        public JavaScriptFileStatsPresenter(IHumanInterface ux, IJavaScriptStatsPresenter javaScriptStatsPresenter, IFilteredFilesEvaluator filteredFilesEvaluator)
        {
            FilteredFilesEvaluator = filteredFilesEvaluator;
            JavaScriptStatsPresenter = javaScriptStatsPresenter;
            Ux = ux;
        }

        public void Present()
        {
            //var file = "";

            //while (!File.Exists(file))
            //{
            //    file = Ux.Read("Enter File");
            //}

            const string file = @"C:\Code\AP2\Accelerate\Achilles.Accelerate.Web\Views\Search\_AdvanceSearch.cshtml";

            JavaScriptStatsPresenter.Present(FilteredFilesEvaluator.Evaluate(new List<string> { file }));
        }
    }
}