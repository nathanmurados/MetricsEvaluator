using System.Collections.Generic;

namespace MetricsUtility.Core.Services.Evaluators
{
    public interface ISettingsEvaluator
    {
        string GetApTwoDirectory();
        List<string> GetSpecificFiles();


    }
}