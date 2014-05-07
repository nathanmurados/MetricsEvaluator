using System.Collections.Generic;
using System.Windows;
using MetricsUtility.Clients.Wpf.Services.Evaluators.Interfaces;
using MetricsUtility.Clients.Wpf.Services.Presenters.Interfaces;
using MetricsUtility.Core.Services;
using MetricsUtility.Core.Services.Evaluators;
using MetricsUtility.Core.Services.Presenters;
using MetricsUtility.Core.Services.Storers;

namespace MetricsUtility.Clients.Wpf.Services.Presenters
{
    public class CssMetricsPresenter : ICssMetricsPresenter, IHasHumanInterface
    {
        public IDirectoryDescendentFilesEvaluator DirectoryDescendentFilesEvaluator { get; private set; }
        public ICssStatsPresenter CssStatsPresenter { get; private set; }
        public IFilteredFilesEvaluator FilteredFilesEvaluator { get; private set; }
        public ICssStatsStorer CssStatsStorer { get; private set; }
        public IHumanInterface Ux { get; private set; }
        public IPathExistenceEvaluator PathExistenceEvaluator { get; private set; }
        public IFilePresenter FilePresenter { get; private set; }

        public CssMetricsPresenter(IHumanInterface ux, IDirectoryDescendentFilesEvaluator directoryDescendentFilesEvaluator, ICssStatsPresenter cssStatsPresenter, IFilteredFilesEvaluator filteredFilesEvaluator, ICssStatsStorer cssStatsStorer, IPathExistenceEvaluator pathExistenceEvaluator, IFilePresenter filePresenter)
        {
            FilePresenter = filePresenter;
            PathExistenceEvaluator = pathExistenceEvaluator;
            CssStatsStorer = cssStatsStorer;
            FilteredFilesEvaluator = filteredFilesEvaluator;
            CssStatsPresenter = cssStatsPresenter;
            DirectoryDescendentFilesEvaluator = directoryDescendentFilesEvaluator;
            Ux = ux;
        }

        public void View(List<string> files)
        {
            if (PathExistenceEvaluator.Evaluate(Properties.Settings.Default.InspectionPath))
            {
                var results = CssStatsPresenter.Present(FilteredFilesEvaluator.Evaluate(files));
                Ux.DisplayBoolOption("Store detailed CSS results to disk?", () =>
                {
                    var filename = CssStatsStorer.Store(results, string.Empty);
                    FilePresenter.Present(filename);
                }, null);

                Ux.WriteLine("");
            }
            else
            {
                MessageBox.Show("Please specify the results folder first.", "Invalid Directory");
            }
        }
    }
}