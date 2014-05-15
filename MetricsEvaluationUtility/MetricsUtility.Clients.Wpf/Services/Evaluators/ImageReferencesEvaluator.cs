using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MetricsUtility.Clients.Wpf.Services.Evaluators.Interfaces;
using MetricsUtility.Core.Services;
using MetricsUtility.Core.Services.Evaluators;

namespace MetricsUtility.Clients.Wpf.Services.Evaluators
{
    public class ImageReferencesEvaluator : IImageReferencesEvaluator, IHasHumanInterface
    {
        public static object Lock = new object();
        public IHumanInterface Ux { get; private set; }
        public IDirectoryDescendentFilesEvaluator DirectoryDescendentFilesEvaluator { get; private set; }

        public ImageReferencesEvaluator(IHumanInterface ux, IDirectoryDescendentFilesEvaluator directoryDescendentFilesEvaluator)
        {
            DirectoryDescendentFilesEvaluator = directoryDescendentFilesEvaluator;
            Ux = ux;
        }

        public void Evaluate()
        {
            var files = DirectoryDescendentFilesEvaluator.Evaluate(Properties.Settings.Default.InspectionPath).Where(x => x.EndsWith(".css")).ToList();
            var count = files.Count;

            double oldPercentage = 0;
            var i = 0;

            var references = new List<string>();

            Parallel.ForEach(files, file =>
            {
                var line = File.ReadAllLines(file).Count(x => (x.Contains("url")));

                Interlocked.Increment(ref i);

                lock (Lock)
                {
                    if (line != 0)
                    {
                        references.Add(file.Replace(Properties.Settings.Default.InspectionPath, "") + ": " + line);
                    }

                    var newPercentage = (double)Math.Round((100m / count) * i);

                    if (newPercentage != oldPercentage)
                    {
                        Ux.UpdateProgress((int)newPercentage);
                        oldPercentage = newPercentage;
                    }
                }
            });

            Ux.WriteLine("Count: " + references.Count);
            foreach (var reference in references.OrderBy(x => x))
            {
                Ux.WriteLine(reference);
            }

            Ux.WriteLine("");

            Ux.ResetProgress();
        }

    }
}