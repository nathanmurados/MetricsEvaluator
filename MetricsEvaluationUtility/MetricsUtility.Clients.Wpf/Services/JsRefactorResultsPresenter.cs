using System.Collections.Generic;
using System.Linq;
using MetricsUtility.Core.Services;

namespace MetricsUtility.Clients.Wpf.Services
{
    public class JsRefactorResultsPresenter : IJsRefactorResultsPresenter, IHasHumanInterface
    {
        public IHumanInterface Ux { get; private set; }

        public void Present(List<string> failedFiles, List<string> avoidedOverWrites)
        {
            if (!failedFiles.Any() && !avoidedOverWrites.Any())
            {
                Ux.WriteLine("Operation complete.");
                Ux.WriteLine("");
            }
            else
            {
                Ux.WriteLine(string.Format("Operation completed with {0} ERRORS.", failedFiles.Count));
                foreach (var failedFile in failedFiles)
                {
                    Ux.WriteLine("Unable to parse: " + failedFile);
                }
                foreach (var collision in avoidedOverWrites)
                {
                    Ux.WriteLine("Skipped overwrite: " + collision);
                }

                Ux.WriteLine("");
            }
        }

    }
}