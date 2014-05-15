using System.Diagnostics;
using MetricsUtility.Clients.Wpf.Services.Evaluators.Interfaces;
using MetricsUtility.Clients.Wpf.Services.Presenters.Interfaces;

namespace MetricsUtility.Clients.Wpf.Services.Presenters
{
    public class FilePresenter : IFilePresenter
    {
        public IFileExistenceEvaluator FileExistenceEvaluator { get; private set; }

        public FilePresenter(IFileExistenceEvaluator fileExistenceEvaluator)
        {
            FileExistenceEvaluator = fileExistenceEvaluator;
        }

        public void Present(string path)
        {
            if (FileExistenceEvaluator.Evaluate(path))
            {
                var explorerWindowProcess = new Process
                {
                    StartInfo =
                    {
                        FileName = "explorer.exe",
                        Arguments = string.Format("/select,\"{0}\"", path)
                    }
                };

                explorerWindowProcess.Start();
            }
        }
    }
}