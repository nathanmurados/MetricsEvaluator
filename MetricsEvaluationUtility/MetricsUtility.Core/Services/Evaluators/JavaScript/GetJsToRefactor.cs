using System;
using System.Collections.Generic;

namespace MetricsUtility.Core.Services.Evaluators.JavaScript
{
    public class GetJsToRefactor : IGetJsToRefactor
    {
        /// <summary>
        /// Extract the razor code from the line of javascript
        /// Input: A line of Javascript containing an @. The @ prefixes razor code
        /// Note the line may contain several fragments of razor, but only the first (working left ro right) should be processed
        /// The razor code is prefixed with @, it will probably be surrounded by quotes (single or double).
        /// </summary>
        public IEnumerable<string> GetFragment(string jsLine)
        {
            throw new NotImplementedException();
        }
    }
}
