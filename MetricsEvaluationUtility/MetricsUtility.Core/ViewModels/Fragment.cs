using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsUtility.Core.ViewModels
{
    public class Fragment
    {
        public String Text { get; set; }

        public FragType FragType { get; set; }
    }

    public enum FragType
    {
        Quoted,
        Unquoted,
        TextLeft,
        TextRight,
        TextLeftRight,
        RequiresManualCheck
    }
}
