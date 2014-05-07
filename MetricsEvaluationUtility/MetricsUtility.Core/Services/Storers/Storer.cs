using System.IO;
using System.Text;

namespace MetricsUtility.Core.Services.Storers
{
    public sealed class Storer : IStorer
    {
        public IResultsDirectoryEvaluator ResultsDirectoryEvaluator { get; set; }

        public void Store(StringBuilder stringBuilder, string fileName)
        {
            var root = ResultsDirectoryEvaluator.Evaluate();

            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }

            File.WriteAllText(root + fileName, stringBuilder.ToString());
        }
    }

    public interface IStorer
    {
        void Store(StringBuilder stringBuilder, string fileName);
    }
}