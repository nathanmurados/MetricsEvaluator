using System.Collections.Generic;

namespace MetricsEvaluationUtility.Services.Evaluators.JavaScript
{
    public class JsAttributesProvider
    {
        public static IEnumerable<string> Attributes = new[] {
                "onclick", "ondblclick", "onmousedown", "onmousemove", "onmouseover", "onmouseout", "onmouseup",
                "onkeydown","onkeypress","onkeyup",
                "onabort","onerror","onload","onresize","onscroll","onunload",
                "onblur","onchange","onfocus","onreset","onselect","onsubmit",
        };
    }
}