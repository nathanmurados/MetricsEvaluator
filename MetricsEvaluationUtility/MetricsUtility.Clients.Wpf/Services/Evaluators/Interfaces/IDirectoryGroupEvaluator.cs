using System.Collections.Generic;
using MetricsUtility.Clients.Wpf.ViewModels;

namespace MetricsUtility.Clients.Wpf.Services.Evaluators.Interfaces
{
    public interface IDirectoryGroupEvaluator
    {
        List<GroupedFilesViewModel> Evaluate(int numberOfGroups, string[] directories);
    }
}