using System.IO;

namespace MetricsUtility.Clients.Wpf.Services.Evaluators
{
    public class PathExistenceEvaluator : IPathExistenceEvaluator
    {
        public bool Evaluate(string path)
        {
            return !string.IsNullOrEmpty(path) && Directory.Exists(path);
        }
    }
}