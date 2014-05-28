using System.Collections.Generic;

namespace MetricsUtility.Core.Services.Evaluators.Css
{
    public interface IRemediatedBlockJsRemover
    {
        List<BlockContent> Remove(IEnumerable<BlockContent> matches);
    }
}