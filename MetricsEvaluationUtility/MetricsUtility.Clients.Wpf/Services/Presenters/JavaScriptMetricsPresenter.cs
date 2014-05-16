using System.Collections.Generic;
using System.IO;
using System.Windows;
using MetricsUtility.Clients.Wpf.Services.Presenters.Interfaces;
using MetricsUtility.Core.Services;
using MetricsUtility.Core.Services.Evaluators;
using MetricsUtility.Core.Services.Presenters;
using MetricsUtility.Core.Services.StorageServices;

namespace MetricsUtility.Clients.Wpf.Services.Presenters
{
    public class JavaScriptMetricsPresenter : IJavaScriptMetricsPresenter, IHasHumanInterface
    {
        public IJavaScriptStatsPresenter JavaScriptStatsPresenter { get; private set; }
        public IFilteredFilesEvaluator FilteredFilesEvaluator { get; private set; }
        public IJavaScriptStatsStorageService CssJavaScriptStorageService { get; private set; }
        public IHumanInterface Ux { get; private set; }
        public IFilePresenter FilePresenter { get; private set; }

        public JavaScriptMetricsPresenter(IHumanInterface ux, IJavaScriptStatsPresenter javaScriptStatsPresenter, IFilteredFilesEvaluator filteredFilesEvaluator, IJavaScriptStatsStorageService cssJavaScriptStorageService, IFilePresenter filePresenter)
        {
            FilePresenter = filePresenter;
            CssJavaScriptStorageService = cssJavaScriptStorageService;
            FilteredFilesEvaluator = filteredFilesEvaluator;
            JavaScriptStatsPresenter = javaScriptStatsPresenter;
            Ux = ux;
        }

        public void View(List<string> files)
        {
            if (Directory.Exists(Properties.Settings.Default.InspectionPath))
            {
                var results = JavaScriptStatsPresenter.Present(FilteredFilesEvaluator.Evaluate(files));
                Ux.DisplayBoolOption("Store detailed JavaScript results to disk?", () => CssJavaScriptStorageService.Store(results, string.Empty), null);

                Ux.WriteLine("");
            }
            else
            {
                MessageBox.Show("Invalid Directory");
            }
        }
    }
}