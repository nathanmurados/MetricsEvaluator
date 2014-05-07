using System.Diagnostics;
using MetricsUtility.Clients.Wpf.Services.Evaluators;
using MetricsUtility.Clients.Wpf.Services.Presenters.Interfaces;

namespace MetricsUtility.Clients.Wpf.Services.Presenters
{
    public class FolderPresenter : IFolderPresenter
    {
        public IPathExistenceEvaluator PathExistenceEvaluator { get; private set; }

        public FolderPresenter(IPathExistenceEvaluator pathExistenceEvaluator)
        {
            PathExistenceEvaluator = pathExistenceEvaluator;
        }

        public void Present(string path)
        {
            if (PathExistenceEvaluator.Evaluate(path))
            {
                Process.Start(path);
            }
        }
    }
}