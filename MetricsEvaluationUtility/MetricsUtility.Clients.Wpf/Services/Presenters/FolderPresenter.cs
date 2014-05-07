using System.Diagnostics;
using MetricsUtility.Clients.Wpf.Services.Evaluators.Interfaces;
using MetricsUtility.Clients.Wpf.Services.Presenters.Interfaces;

namespace MetricsUtility.Clients.Wpf.Services.Presenters
{
    public class FolderPresenter : IFolderPresenter
    {
        public IFolderExistenceEvaluator FolderExistenceEvaluator { get; private set; }

        public FolderPresenter(IFolderExistenceEvaluator folderExistenceEvaluator)
        {
            FolderExistenceEvaluator = folderExistenceEvaluator;
        }

        public void Present(string path)
        {
            if (FolderExistenceEvaluator.Evaluate(path))
            {
                var explorerWindowProcess = new Process
                {
                    StartInfo =
                    {
                        FileName = "explorer.exe", 
                        Arguments = path
                    }
                };

                explorerWindowProcess.Start();
            }
        }
    }
}