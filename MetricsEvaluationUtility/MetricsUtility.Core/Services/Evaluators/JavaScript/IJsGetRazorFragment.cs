using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetricsUtility.Core.Services.Evaluators.JavaScript
{
    public interface IJsGetRazorFragment
    {
        string GetFragment(string jsLine);
    }
}
