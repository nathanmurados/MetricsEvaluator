using System.Collections.Generic;
using System.Linq;
using MetricsUtility.Clients.Wpf.Services.Evaluators.Interfaces;

namespace MetricsUtility.Clients.Wpf.Services.Evaluators
{
    public class FilesToInspectEvaluator : IFilesToInspectEvaluator
    {
        public List<string> Evaluate()
        {
            return string.IsNullOrWhiteSpace(Properties.Settings.Default.LastFiles) ? new List<string>() : Properties.Settings.Default.LastFiles.Split('~').ToList();
        }
    }
}