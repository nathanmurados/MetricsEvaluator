using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using MetricsUtility.Core.Services;
using MetricsUtility.Core.Services.Refactorers;

namespace MetricsUtility.Clients.Wpf.Services
{
    public class JsSplitter : IJsSplitter, IHasHumanInterface
    {
        public IHumanInterface Ux { get; private set; }
        public IPageJsSeperationEvaluator PageJsSeperationEvaluator { get; private set; }
        public IDirectoryMimicker DirectoryMimicker { get; private set; }
        public IJsRefactorResultsPresenter JsRefactorResultsPresenter { get; private set; }
        public ISplitJsFileCreator SplitJsFileCreator { get; private set; }

        public JsSplitter(IHumanInterface ux, IPageJsSeperationEvaluator pageJsSeperationEvaluator, IDirectoryMimicker directoryMimicker, IJsRefactorResultsPresenter jsRefactorResultsPresenter, ISplitJsFileCreator splitJsFileCreator)
        {
            SplitJsFileCreator = splitJsFileCreator;
            JsRefactorResultsPresenter = jsRefactorResultsPresenter;
            DirectoryMimicker = directoryMimicker;
            PageJsSeperationEvaluator = pageJsSeperationEvaluator;
            Ux = ux;
        }

        public void Split(string refactorPath, string generatedFilesPath, string[] filesToRefactor)
        {
            var failedFiles = new List<string>();
            var avoidedOverWrites = new List<string>();
            var filesCreated = 0;

            for (var i = 0; i < filesToRefactor.Length; i++)
            {
                var file = filesToRefactor[i];
                var newPath = DirectoryMimicker.Mimick(refactorPath, generatedFilesPath, file);

                SeperatedJsViewModel seperatedJsViewModel;

                Ux.WriteLine(string.Format("Processing item {0}", i.ToString(CultureInfo.InvariantCulture)));

                try
                {
                    seperatedJsViewModel = PageJsSeperationEvaluator.Evaluate(File.ReadAllLines(file), Properties.Settings.Default.SolutionPath, newPath, file);
                }
                catch (Exception e)
                {
                    failedFiles.Add(file);
                    continue;
                }

                SplitJsFileCreator.Create(seperatedJsViewModel, newPath, avoidedOverWrites, ref filesCreated, file);
            }

            Ux.WriteLine(string.Format("Created {0} failedFiles", filesCreated));

            JsRefactorResultsPresenter.Present(failedFiles, avoidedOverWrites);
        }
    }
}