using System.IO;
using MetricsUtility.Clients.Wpf.Services.Evaluators.Interfaces;

namespace MetricsUtility.Clients.Wpf.Services.Evaluators
{
    public class FileExistenceEvaluator : IFileExistenceEvaluator
    {
        public bool Evaluate(string path)
        {
            return !string.IsNullOrEmpty(path) && File.Exists(path);
        }
    }
}