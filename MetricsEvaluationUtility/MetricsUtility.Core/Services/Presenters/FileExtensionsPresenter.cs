using System.Collections.Generic;
using System.Linq;

namespace MetricsUtility.Core.Services.Presenters
{
    public class FileExtensionsPresenter : IFileExtensionPresenter
    {
        public IHumanInterface Ux { get; private set; }

        public FileExtensionsPresenter(IHumanInterface ux)
        {
            Ux = ux;
        }

        public void Present(IEnumerable<string> files)
        {
            var extensions = new List<string>();

            foreach (var parts in files.Select(file => file.Split('.')).Where(parts => extensions.All(x => x != parts[parts.Length - 1])))
            {
                extensions.Add(parts[parts.Length - 1]);
            }

            foreach (var extension in extensions.OrderBy(x => x))
            {
                Ux.WriteLine(extension);
            }

            Ux.WriteLine("Total Extensions: " + extensions.Count());
        }
    }

    public interface IFileExtensionPresenter : IPresenter
    {
        void Present(IEnumerable<string> files);
    }
}