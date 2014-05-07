using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MetricsUtility.Core.Services.Evaluators
{
    public class DirectoryDescendentFilesEvaluator : IDirectoryDescendentFilesEvaluator
    {
        public IEnumerable<string> Evaluate(string directory)
        {
            var files = Directory.GetFiles(directory).ToList();

            foreach (var childDirectory in Directory.GetDirectories(directory))
            {
                files.AddRange(Evaluate(childDirectory));
            }

            return files;
        }
    }

    public interface IDirectoryDescendentFilesEvaluator
    {
        IEnumerable<string> Evaluate(string directory);
    }
}