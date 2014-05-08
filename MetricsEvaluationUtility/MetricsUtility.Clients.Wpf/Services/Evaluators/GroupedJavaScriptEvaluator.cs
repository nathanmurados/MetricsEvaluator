using System;
using System.Collections.Generic;
using MetricsUtility.Clients.Wpf.Services.Evaluators.Interfaces;
using MetricsUtility.Clients.Wpf.Services.Presenters.Interfaces;
using MetricsUtility.Core.Services;
using MetricsUtility.Core.Services.Presenters;
using MetricsUtility.Core.Services.Storers;
using MetricsUtility.Core.ViewModels;

namespace MetricsUtility.Clients.Wpf.Services.Evaluators
{
    public class GroupedJavaScriptEvaluator : IGroupedJavaScriptEvaluator, IHasHumanInterface
    {
        public IPathExistenceEvaluator PathExistenceEvaluator { get; private set; }
        public IDirectoryGroupEvaluator DirectoryGroupEvaluator { get; private set; }
        public IHumanInterface Ux { get; private set; }
        public IJavaScriptStatsPresenter JavaScriptStatsPresenter { get; private set; }
        public IJavaScriptStatsStorer JavaScriptStatsStorer { get; private set; }
        public IFolderPresenter FolderPresenter { get; private set; }
        public event EventHandler ScrollDown;


        public GroupedJavaScriptEvaluator(IPathExistenceEvaluator pathExistenceEvaluator, IDirectoryGroupEvaluator directoryGroupEvaluator, IHumanInterface ux, IJavaScriptStatsPresenter javaScriptStatsPresenter, IJavaScriptStatsStorer javaScriptStatsStorer, IFolderPresenter folderPresenter)
        {
            FolderPresenter = folderPresenter;
            JavaScriptStatsStorer = javaScriptStatsStorer;
            JavaScriptStatsPresenter = javaScriptStatsPresenter;
            Ux = ux;
            DirectoryGroupEvaluator = directoryGroupEvaluator;
            PathExistenceEvaluator = pathExistenceEvaluator;
        }

        public void Evaluate(int numberOfGroups, string[] directories, int specificGroup)
        {
            var groupedFilesList = DirectoryGroupEvaluator.Evaluate(numberOfGroups, directories);

            var groupedResults = new List<List<JavaScriptEvaluationResult>>();

            var i = 1;
            foreach (var fileList in groupedFilesList)
            {
                if (specificGroup == 0 || i == specificGroup)
                {
                    Ux.WriteLine(string.Format("Group{0} ({1} {2})", i, fileList.StartDir, fileList.EndDir));
                    groupedResults.Add(JavaScriptStatsPresenter.Present(fileList.Files));
                    Ux.WriteLine("");
                    ScrollDown(null, null);
                }
                i++;
            }

            Ux.DisplayBoolOption("Store detailed JS results to disk?", () =>
            {
                i = 1;
                foreach (var resultGroup in groupedResults)
                {
                    JavaScriptStatsStorer.Store(resultGroup, string.Format("Group {0}", specificGroup > 0 ? specificGroup : i));
                    ScrollDown(null, null);
                    i++;
                }
                //FolderPresenter.Present(Properties.Settings.Default.ResultsPath);

            }, null);

            Ux.WriteLine("");
        }
    }
}