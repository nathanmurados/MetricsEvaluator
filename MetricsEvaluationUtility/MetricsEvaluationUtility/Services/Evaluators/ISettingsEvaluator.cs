using System.Collections.Generic;

namespace MetricsEvaluationUtility.Services.Evaluators
{
    public interface ISettingsEvaluator
    {
        string GetApTwoDirectory();
        List<string> GetSpecificFiles();
    }
}