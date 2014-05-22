using System.Collections.Generic;
using System.IO;
using System.Linq;
using MetricsUtility.Core.Services;
using MetricsUtility.Core.Services.RefactorServices;

namespace MetricsUtility.Clients.Wpf.Services
{
    public class SplitJsFileCreator : ISplitJsFileCreator, IHasHumanInterface
    {
        public IHumanInterface Ux { get; private set; }

        public SplitJsFileCreator(IHumanInterface ux)
        {
            Ux = ux;
        }

        public void Create(SeperatedJs seperatedJs, string newPath, List<string> avoidedOverWrites, ref int filesCreated, string file, bool reportAtsigns)
        {
            if (seperatedJs.JsRemoved.Any())
            {
                foreach (var newFile in seperatedJs.JsRemoved)
                {
                    var uri = newPath + "\\" + newFile.ProposedFileName;

                    if (!Directory.Exists(newPath))
                    {
                        Directory.CreateDirectory(newPath);
                    }

                    if (File.Exists(uri))
                    {
                        avoidedOverWrites.Add(uri);
                        Ux.WriteLine(string.Format("SKIPPED: {0}", uri));
                        continue;
                    }

                    File.WriteAllLines(uri, newFile.Lines);

                    var atSigns = newFile.Lines.Count(x => x.Contains("@"));
                    var dotDotSlashes = newFile.Lines.Count(x => x.Contains("../"));
                    if (atSigns > 0 && reportAtsigns)
                    {
                        Ux.WriteLine(string.Format("---WARNING: {0} lines containing @ were detected", atSigns));
                    }
                    if (dotDotSlashes > 0)
                    {
                        Ux.WriteLine(string.Format("---WARNING: {0} lines containing ../ were detected", dotDotSlashes));
                    }

                    Ux.WriteLine("Created " + uri);
                    filesCreated++;
                }
                File.WriteAllLines(file, seperatedJs.RefactoredLines);
            }
        }

    }
}