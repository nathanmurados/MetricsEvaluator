using MetricsUtility.Core.Services.Evaluators;
using MetricsUtility.Core.Services.Storers;

namespace MetricsUtility.Core.Services.Presenters
{
    public class JavaScriptFileStatsPresenter : IJavaScriptFileStatsPresenter
    {
        public IHumanInterface Ux { get; private set; }
        public IFilteredFilesEvaluator FilteredFilesEvaluator { get; private set; }
        public IJavaScriptStatsPresenter JavaScriptStatsPresenter { get; private set; }
        public ISettingsEvaluator SettingsEvaluator { get; private set; }
        public ICssStatsPresenter CssStatsPresenter { get; private set; }
        public IJavaScriptStatsStorer JavaScriptStatsStorer { get; private set; }
        public ICssStatsStorer CssStatsStorer { get; private set; }

        public JavaScriptFileStatsPresenter(IHumanInterface ux, IJavaScriptStatsPresenter javaScriptStatsPresenter, IFilteredFilesEvaluator filteredFilesEvaluator, ISettingsEvaluator settingsEvaluator, ICssStatsPresenter cssStatsPresenter, IJavaScriptStatsStorer javaScriptStatsStorer, ICssStatsStorer cssStatsStorer)
        {
            CssStatsStorer = cssStatsStorer;
            JavaScriptStatsStorer = javaScriptStatsStorer;
            CssStatsPresenter = cssStatsPresenter;
            SettingsEvaluator = settingsEvaluator;
            FilteredFilesEvaluator = filteredFilesEvaluator;
            JavaScriptStatsPresenter = javaScriptStatsPresenter;
            Ux = ux;
        }

        public void Present()
        {
            var jsResults = JavaScriptStatsPresenter.Present(FilteredFilesEvaluator.Evaluate(SettingsEvaluator.GetSpecificFiles()));

            var cssResults = CssStatsPresenter.Present(FilteredFilesEvaluator.Evaluate(SettingsEvaluator.GetSpecificFiles()));

            Ux.DisplayBoolOption("Store detailed Javascript results to disk?", () => JavaScriptStatsStorer.Store(jsResults), null);
            Ux.DisplayBoolOption("Store detailed CSS results to disk?", () => CssStatsStorer.Store(cssResults), null);
        }
    }
}