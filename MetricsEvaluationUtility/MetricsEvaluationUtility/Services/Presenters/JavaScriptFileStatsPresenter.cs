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
        public ISettingsEvaluator SettingsEvaluator { get; private set; }

        public JavaScriptFileStatsPresenter(IHumanInterface ux, IJavaScriptStatsPresenter javaScriptStatsPresenter, IFilteredFilesEvaluator filteredFilesEvaluator, ISettingsEvaluator settingsEvaluator)
        {
            SettingsEvaluator = settingsEvaluator;
            FilteredFilesEvaluator = filteredFilesEvaluator;
            JavaScriptStatsPresenter = javaScriptStatsPresenter;
            Ux = ux;
        }

        public void Present()
        {
            JavaScriptStatsPresenter.Present(FilteredFilesEvaluator.Evaluate(SettingsEvaluator.GetSpecificFiles()));
        }
    }
}