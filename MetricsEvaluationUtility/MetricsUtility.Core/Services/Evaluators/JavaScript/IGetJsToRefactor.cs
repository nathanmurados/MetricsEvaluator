using System.Collections.Generic;

namespace MetricsUtility.Core.Services.Evaluators.JavaScript
{
    public interface IGetJsToRefactor
    {
        IEnumerable<string> GetFragment(string jsLine);
    }
}
