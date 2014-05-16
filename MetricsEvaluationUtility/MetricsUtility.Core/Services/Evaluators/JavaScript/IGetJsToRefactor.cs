using System.Collections.Generic;

namespace MetricsUtility.Core.Services.Evaluators.JavaScript
{
    public interface IGetJsToRefactor
    {
        IEnumerable<string> Evaluate(string jsLine);
    }
}
