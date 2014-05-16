using MetricsUtility.Core.Services.Evaluators;
using MetricsUtility.Core.Services.StorageServices;

namespace MetricsUtility.Core.Services.Presenters
{
    public class JavaScriptFileStatsPresenter : IJavaScriptFileStatsPresenter
    {
        public IHumanInterface Ux { get; private set; }
        public IFilteredFilesEvaluator FilteredFilesEvaluator { get; private set; }
        public IJavaScriptStatsPresenter JavaScriptStatsPresenter { get; private set; }
        public ISettingsEvaluator SettingsEvaluator { get; private set; }
        public ICssStatsPresenter CssStatsPresenter { get; private set; }
        public IJavaScriptStatsStorageService JavaScriptStatsStorageService { get; private set; }
        public ICssStatsStorageService CssStatsStorageService { get; private set; }

        public JavaScriptFileStatsPresenter(IHumanInterface ux, IJavaScriptStatsPresenter javaScriptStatsPresenter, IFilteredFilesEvaluator filteredFilesEvaluator, ISettingsEvaluator settingsEvaluator, ICssStatsPresenter cssStatsPresenter, IJavaScriptStatsStorageService javaScriptStatsStorageService, ICssStatsStorageService cssStatsStorageService)
        {
            CssStatsStorageService = cssStatsStorageService;
            JavaScriptStatsStorageService = javaScriptStatsStorageService;
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

            Ux.DisplayBoolOption("Store detailed Javascript results to disk?", () => JavaScriptStatsStorageService.Store(jsResults, string.Empty), null);
            Ux.DisplayBoolOption("Store detailed CSS results to disk?", () => CssStatsStorageService.Store(cssResults,string.Empty), null);
        }
    }
}