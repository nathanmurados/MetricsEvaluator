using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsUtility.Core.Services.Evaluators.JavaScript
{
    public interface IJsVariableNameEvaluator
    {
        string Evaluate(string razorCode);
    }
}
