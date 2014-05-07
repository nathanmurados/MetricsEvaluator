using System.IO;
using System.Text;

namespace MetricsUtility.Core.Services.Storers
{
    public sealed class Storer : IStorer
    {
        public IResultsDirectoryEvaluator ResultsDirectoryEvaluator { get; private set; }

        public Storer(IResultsDirectoryEvaluator resultsDirectoryEvaluator)
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

    public interface IStorer
    {
        string Store(StringBuilder stringBuilder, string fileName);
    }
}