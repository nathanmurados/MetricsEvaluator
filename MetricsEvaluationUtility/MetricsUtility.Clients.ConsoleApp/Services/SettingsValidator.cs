using System.IO;
using System.Text;
using MetricsUtility.Core.Services;
using MetricsUtility.Core.Services.Storers;

namespace MetricsUtility.Clients.ConsoleApp.Services
{
    public class SettingsValidator : ISettingsValidator
    {
        public string Dir { get { return @"C:\MetricsEvaluationUtility\Settings\"; } }
        public string SettingsFile { get { return "settings.dat"; } }
        public string SettingsPath { get { return Dir + SettingsFile; } }
        public string SettingsHeader { get { return "Specific files to inspect:"; } }
        public string Ap2SolutionHeader { get { return "AP2 Solution path:"; } }

        public void Validate()
        {
            if (!Directory.Exists(Dir))
            {
                Directory.CreateDirectory(Dir);
            }

            if (!File.Exists(SettingsPath))
            {
                var sb = new StringBuilder();

                sb.AppendLine(Ap2SolutionHeader);
                sb.AppendLine(@"C:\Code\AP2");
                sb.AppendLine();
                sb.AppendLine(SettingsHeader);
                sb.AppendLine(@"C:\Code\AP2\Accelerate\Achilles.Accelerate.Web\Views\Search\_AdvanceSearch.cshtml");

                using (var fs = File.Create(SettingsPath))
                {
                    var info = new UTF8Encoding(true).GetBytes(sb.ToString());
                    fs.Write(info, 0, info.Length);
                }
            }
        }
    }

    public class CssStatsFileNameEvaluator : ICssStatsFileNameEvaluator ,IHasDateTimeProvider
    {
        public IDateTimeProvider DateTimeProvider { get; private set; }

        public CssStatsFileNameEvaluator(IDateTimeProvider dateTimeProvider)
        {
            DateTimeProvider = dateTimeProvider;
        }
        
        public string Evaluate()
        {
            return "Css Validation Results " + DateTimeProvider.Now.ToString("yy-MM-dd HH.mm.ss") + ".csv";
        }
    }
}