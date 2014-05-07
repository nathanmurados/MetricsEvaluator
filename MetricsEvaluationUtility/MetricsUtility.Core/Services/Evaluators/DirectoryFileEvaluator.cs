using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MetricsUtility.Core.Services.Evaluators
{
    public class DirectoryFileEvaluator : IDirectoryFileEvaluator
    {
        public IEnumerable<string> GetFiles(string directory)
        {
            var files = Directory.GetFiles(directory).ToList();

            foreach (var childDirectory in Directory.GetDirectories(directory))
            {
                files.AddRange(GetFiles(childDirectory));
            }

            return files;
        }
    }

    public interface IDirectoryFileEvaluator
    {
        IEnumerable<string> GetFiles(string directory);
    }
}