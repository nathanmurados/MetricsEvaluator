using System.IO;
using System.Linq;
using System.Windows;
using MetricsUtility.Clients.Wpf.Services.Evaluators;
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
        public IFolderPresenter FolderPresenter { get; private set; }
        public IPathExistenceEvaluator PathExistenceEvaluator { get; private set; }

        public CssMetricsPresenter(IHumanInterface ux, IDirectoryFileEvaluator directoryFileEvaluator, ICssStatsPresenter cssStatsPresenter, IFilteredFilesEvaluator filteredFilesEvaluator, ICssStatsStorer cssStatsStorer, IFolderPresenter folderPresenter, IPathExistenceEvaluator pathExistenceEvaluator)
        {
            PathExistenceEvaluator = pathExistenceEvaluator;
            FolderPresenter = folderPresenter;
            CssStatsStorer = cssStatsStorer;
            FilteredFilesEvaluator = filteredFilesEvaluator;
            CssStatsPresenter = cssStatsPresenter;
            DirectoryFileEvaluator = directoryFileEvaluator;
            Ux = ux;
        }

        public void View()
        {
            var path = Properties.Settings.Default.InspectionPath;

            if (PathExistenceEvaluator.Evaluate(path))
            {
                var files = DirectoryFileEvaluator.GetFiles(path).OrderBy(x => x).ToList();

                var results = CssStatsPresenter.Present(FilteredFilesEvaluator.Evaluate(files));
                Ux.DisplayBoolOption("Store detailed CSS results to disk?", () =>
                {
                    CssStatsStorer.Store(results);
                    FolderPresenter.Present(path);
                }, null);
            }
            else
            {
                MessageBox.Show("Please specify the results folder first.","Invalid Directory");
            }
        }
    }
}