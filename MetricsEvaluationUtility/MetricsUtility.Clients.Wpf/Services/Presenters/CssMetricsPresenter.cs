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
        public IDirectoryFileEvaluator DirectoryFileEvaluator { get; private set; }
        public ICssStatsPresenter CssStatsPresenter { get; private set; }
        public IFilteredFilesEvaluator FilteredFilesEvaluator { get; private set; }
        public ICssStatsStorer CssStatsStorer { get; private set; }
        public IHumanInterface Ux { get; private set; }
        public IFolderExistenceEvaluator FolderExistenceEvaluator { get; private set; }
        public IFilePresenter FilePresenter { get; private set; }

        public CssMetricsPresenter(IHumanInterface ux, IDirectoryFileEvaluator directoryFileEvaluator, ICssStatsPresenter cssStatsPresenter, IFilteredFilesEvaluator filteredFilesEvaluator, ICssStatsStorer cssStatsStorer, IFolderExistenceEvaluator folderExistenceEvaluator, IFilePresenter filePresenter)
        {
            FilePresenter = filePresenter;
            FolderExistenceEvaluator = folderExistenceEvaluator;
            CssStatsStorer = cssStatsStorer;
            FilteredFilesEvaluator = filteredFilesEvaluator;
            CssStatsPresenter = cssStatsPresenter;
            DirectoryFileEvaluator = directoryFileEvaluator;
            Ux = ux;
        }

        public void View(List<string> files)
        {
            if (FolderExistenceEvaluator.Evaluate(Properties.Settings.Default.InspectionPath))
            {
                var results = CssStatsPresenter.Present(FilteredFilesEvaluator.Evaluate(files));
                Ux.DisplayBoolOption("Store detailed CSS results to disk?", () =>
                {
                    var filename = CssStatsStorer.Store(results);
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