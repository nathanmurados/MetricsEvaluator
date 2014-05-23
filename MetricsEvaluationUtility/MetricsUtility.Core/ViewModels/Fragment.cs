using System;

namespace MetricsUtility.Core.ViewModels
{
    public class Fragment
    {
        public String Text { get; set; }

        public FragType FragType { get; set; }
    }

    public enum FragType
    {
        Default,
        Quoted,
        Unquoted,
        TextLeft,
        TextRight,
        TextLeftRight,
        RequiresManualCheck,
        SingleQuotes,
        DoubleQuotes
    }
}
