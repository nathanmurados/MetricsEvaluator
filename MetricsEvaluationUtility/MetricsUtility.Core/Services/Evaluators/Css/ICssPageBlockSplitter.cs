using System.Collections.Generic;

namespace MetricsUtility.Core.Services.Evaluators.Css
{
    public interface ICssPageBlockSplitter
    {
        List<PageBlockSplitResult> Split(string[] lines, bool includeBlocksWithAtVars);
    }
}