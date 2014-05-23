using System;
using System.Collections.Generic;
using System.IO;
using MetricsUtility.Core.Services;
using MetricsUtility.Core.Services.RefactorServices;

namespace MetricsUtility.Clients.Wpf.Services
{
    public class AdvancedJsSplitter : IAdvancedJsSplitter, IHasHumanInterface
    {
        public IHumanInterface Ux { get; private set; }
        public IAdvancedJsSeperationService AdvancedJsSeperationService { get; private set; }
        public IDirectoryMimicker DirectoryMimicker { get; private set; }
        public IJsRefactorResultsPresenter JsRefactorResultsPresenter { get; private set; }
        public ISplitJsFileCreator SplitJsFileCreator { get; private set; }

        public AdvancedJsSplitter(IHumanInterface ux, IAdvancedJsSeperationService advancedJsSeperationService, IDirectoryMimicker directoryMimicker, IJsRefactorResultsPresenter jsRefactorResultsPresenter, ISplitJsFileCreator splitJsFileCreator)
        {
            SplitJsFileCreator = splitJsFileCreator;
            JsRefactorResultsPresenter = jsRefactorResultsPresenter;
            DirectoryMimicker = directoryMimicker;
            AdvancedJsSeperationService = advancedJsSeperationService;
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

                SeperatedJs seperatedJs;

                //Ux.WriteLine(string.Format("Processing item {0}", i.ToString(CultureInfo.InvariantCulture)));

                try
                {
                    seperatedJs = AdvancedJsSeperationService.Evaluate(File.ReadAllLines(file), Properties.Settings.Default.SolutionPath, newPath, file);
                }
                catch (Exception e)
                {
                    //Ux.WriteLine(string.Format("{0} ({1})", e.Message, file));
                    failedFiles.Add(file);
                    continue;
                }

                SplitJsFileCreator.Create(seperatedJs, newPath, avoidedOverWrites, ref filesCreated, file, false);
            }

            Ux.WriteLine(string.Format("Refactored {0} files", filesCreated));

            JsRefactorResultsPresenter.Present(failedFiles, avoidedOverWrites);
        }
    }
}