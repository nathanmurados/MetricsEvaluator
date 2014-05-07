using System.IO;
using MetricsUtility.Clients.Wpf.Services.Evaluators.Interfaces;

namespace MetricsUtility.Clients.Wpf.Services.Evaluators
{
    public class FolderExistenceEvaluator : IFolderExistenceEvaluator
    {
        public bool Evaluate(string path)
        {
            return !string.IsNullOrEmpty(path) && Directory.Exists(path);
        }
    }
}