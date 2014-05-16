namespace MetricsUtility.Core.Services.Evaluators
{
    using System.Collections.Generic;

    public interface IDirectoryDescendentFilesEvaluator
    {
        IEnumerable<string> Evaluate(string directory);
    }
}