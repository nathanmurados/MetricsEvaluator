using System.Collections.Generic;
using System.Linq;

namespace MetricsUtility.Core.Services.Evaluators.Css
{
    public class BlockContent
    {
        public List<string> Lines { get; set; }
        public int FirstOccurenceLineNumber { get; set; }
        public int AtSymbols { get; set; }
    }
}