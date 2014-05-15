using System;
using System.Linq;

namespace MetricsUtility.Clients.Wpf.Services
{
    public class DirectoryMimicker : IDirectoryMimicker
    {
        public string Mimick(string refactorPath, string generatedFilesPath, string file)
        {
            if (refactorPath.EndsWith("\\")) throw new NotImplementedException();

            var bit = file.Replace(refactorPath, "");

            var parts = file.Split('\\');
            var fileName = parts.Last();

            var newPath = bit.Replace(fileName, "");

            var newPathParts = newPath.Split('\\');
            newPath = string.Join("\\", newPathParts.Take(newPathParts.Length - 1));

            return string.Format("{0}{1}", generatedFilesPath, newPath);
        }
    }
}