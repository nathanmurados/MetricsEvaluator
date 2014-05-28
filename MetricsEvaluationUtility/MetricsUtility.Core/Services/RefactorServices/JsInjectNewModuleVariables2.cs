using System.Collections.Generic;
using System.Linq;
using MetricsUtility.Core.ViewModels;

namespace MetricsUtility.Core.Services.RefactorServices
{
    public class JsInjectNewModuleVariables2 : IJsInjectNewModuleVariables
    {
        public RazorToJsConvertor RazorToJsConvertor { get; set; }

        public JsInjectNewModuleVariables2()
        {
            RazorToJsConvertor = new RazorToJsConvertor();
        }

        public List<string> Build(List<string> lines, List<JsModuleViewModel> razorVariables)
        {
            var output = new List<string>();

            foreach (var line in lines)
            {
                var newLine = line.Replace("<script type=\"text/javascript\">", "").Replace("</script>", "");

                RazorToJsConvertor.RazorVariables = razorVariables;

                if (razorVariables.Any(x => line.Contains(x.OriginalRazorText)))
                {
                    newLine = RazorToJsConvertor.Convert(newLine);
                }
                
                output.Add(newLine);
            }

            return output;
        }
    }
}