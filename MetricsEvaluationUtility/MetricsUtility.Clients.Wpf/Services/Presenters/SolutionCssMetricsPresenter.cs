using System.IO;
using System.Linq;
using System.Windows;
using MetricsUtility.Core.Services;
using MetricsUtility.Core.Services.Evaluators;
using MetricsUtility.Core.Services.Presenters;
using MetricsUtility.Core.Services.Storers;

namespace MetricsUtility.Clients.Wpf.Services.Presenters
{
    public class SolutionCssMetricsPresenter : ISolutionCssMetricsPresenter, IHasHumanInterface
    {
        public SolutionCssMetricsPresenter(IHumanInterface ux)
        {
            Ux = ux;
        }

        public IDirectoryFileEvaluator DirectoryFileEvaluator { get; private set; }
        public ICssStatsPresenter CssStatsPresenter { get; private set; }
        public IFilteredFilesEvaluator FilteredFilesEvaluator { get; private set; }
        public ICssStatsStorer CssStatsStorer { get; private set; }
        public IHumanInterface Ux { get; private set; }
        
        public void View()
        {
            if (File.Exists(Properties.Settings.Default.SolutionToAnalyse))
            {
                var files =
                    DirectoryFileEvaluator.GetFiles(Properties.Settings.Default.SolutionToAnalyse)
                        .OrderBy(x => x)
                        .ToList();

                var results = CssStatsPresenter.Present(FilteredFilesEvaluator.Evaluate(files));
                Ux.DisplayBoolOption("Store detailed CSS results to disk?", () => CssStatsStorer.Store(results), null);
            }
            else
            {
                MessageBox.Show("Invalid Directory");
            }
        }
    }
}