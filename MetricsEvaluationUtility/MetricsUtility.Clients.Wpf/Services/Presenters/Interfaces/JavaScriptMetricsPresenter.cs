using System.IO;
using System.Linq;
using System.Windows;
using MetricsUtility.Core.Services;
using MetricsUtility.Core.Services.Evaluators;
using MetricsUtility.Core.Services.Presenters;
using MetricsUtility.Core.Services.Storers;

namespace MetricsUtility.Clients.Wpf.Services.Presenters.Interfaces
{
    public class JavaScriptMetricsPresenter : IJavaScriptMetricsPresenter, IHasHumanInterface
    {
        public IDirectoryFileEvaluator DirectoryFileEvaluator { get; private set; }
        public IJavaScriptStatsPresenter JavaScriptStatsPresenter { get; private set; }
        public IFilteredFilesEvaluator FilteredFilesEvaluator { get; private set; }
        public IJavaScriptStatsStorer CssJavaScriptStorer { get; private set; }
        public IHumanInterface Ux { get; private set; }

        public JavaScriptMetricsPresenter(IHumanInterface ux, IDirectoryFileEvaluator directoryFileEvaluator, IJavaScriptStatsPresenter javaScriptStatsPresenter, IFilteredFilesEvaluator filteredFilesEvaluator, IJavaScriptStatsStorer cssJavaScriptStorer)
        {
            CssJavaScriptStorer = cssJavaScriptStorer;
            FilteredFilesEvaluator = filteredFilesEvaluator;
            JavaScriptStatsPresenter = javaScriptStatsPresenter;
            DirectoryFileEvaluator = directoryFileEvaluator;
            Ux = ux;
        }

        public void View()
        {
            if (Directory.Exists(Properties.Settings.Default.InspectionPath))
            {
                var files = DirectoryFileEvaluator.GetFiles(Properties.Settings.Default.InspectionPath).OrderBy(x => x).ToList();

                var results = JavaScriptStatsPresenter.Present(FilteredFilesEvaluator.Evaluate(files));
                Ux.DisplayBoolOption("Store detailed JavaScript results to disk?", () => CssJavaScriptStorer.Store(results), null);
            }
            else
            {
                MessageBox.Show("Invalid Directory");
            }
        }
    }
}