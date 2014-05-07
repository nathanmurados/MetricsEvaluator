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

        public void Store(StringBuilder stringBuilder, string fileName)
        {
            var root = ResultsDirectoryEvaluator.Evaluate();

            if (!string.IsNullOrWhiteSpace(root))
            {
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }
                File.WriteAllText(root + "\\" + fileName, stringBuilder.ToString());
            }
        }
    }

    public interface IStorer
    {
        void Store(StringBuilder stringBuilder, string fileName);
    }
}