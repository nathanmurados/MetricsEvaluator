using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using MetricsUtility.Clients.Wpf.Services.Evaluators.Interfaces;
using MetricsUtility.Clients.Wpf.Services.Presenters.Interfaces;
using MetricsUtility.Core.Services;
using MetricsUtility.Core.Services.Presenters;
using MetricsUtility.Core.Services.Storers;
using MetricsUtility.Core.ViewModels;

namespace MetricsUtility.Clients.Wpf.Services.Evaluators
{
    public class GroupedCssEvaluator : IGroupedCssEvaluator, IHasHumanInterface
    {
        public IPathExistenceEvaluator PathExistenceEvaluator { get; private set; }
        public IDirectoryGroupEvaluator DirectoryGroupEvaluator { get; private set; }
        public IHumanInterface Ux { get; private set; }
        public ICssStatsPresenter CssStatsPresenter { get; private set; }
        public ICssStatsStorer CssStatsStorer { get; private set; }
        public IFolderPresenter FolderPresenter { get; private set; }

        public GroupedCssEvaluator(IPathExistenceEvaluator pathExistenceEvaluator, IDirectoryGroupEvaluator directoryGroupEvaluator, IHumanInterface ux, ICssStatsPresenter cssStatsPresenter, ICssStatsStorer cssStatsStorer, IFolderPresenter folderPresenter)
        {
            FolderPresenter = folderPresenter;
            CssStatsStorer = cssStatsStorer;
            CssStatsPresenter = cssStatsPresenter;
            Ux = ux;
            DirectoryGroupEvaluator = directoryGroupEvaluator;
            PathExistenceEvaluator = pathExistenceEvaluator;
        }

        public void Evaluate(int numberOfGroups, string[] directories)
        {
            var groupedFilesList = DirectoryGroupEvaluator.Evaluate(numberOfGroups, directories);

            var groupedResults = new List<List<CssEvaluationResult>>();
            
            foreach (var fileList in groupedFilesList)
            {
                groupedResults.Add(CssStatsPresenter.Present(fileList));
            }

            Ux.DisplayBoolOption("Store detailed CSS results to disk?", () =>
            {
                foreach (var resultGroup in groupedResults)
                {
                    CssStatsStorer.Store(resultGroup);
                }
            }, null);

            FolderPresenter.Present(Properties.Settings.Default.ResultsPath);
            Ux.WriteLine("");
        }
    }
}
