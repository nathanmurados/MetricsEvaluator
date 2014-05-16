using System.IO;
using System.Text;

namespace MetricsUtility.Core.Services.StorageServices
{
    public sealed class StorageService : IStorageService
    {
        public IResultsDirectoryEvaluator ResultsDirectoryEvaluator { get; private set; }

        public StorageService(IResultsDirectoryEvaluator resultsDirectoryEvaluator)
        {
            ResultsDirectoryEvaluator = resultsDirectoryEvaluator;
        }

        public string Store(StringBuilder stringBuilder, string fileName)
        {
            var root = ResultsDirectoryEvaluator.Evaluate();

            if (!string.IsNullOrWhiteSpace(root))
            {
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }
                var filename = root + "\\" + fileName;
                File.WriteAllText(filename, stringBuilder.ToString());
                return filename;
            }

            return null;
        }
    }
}