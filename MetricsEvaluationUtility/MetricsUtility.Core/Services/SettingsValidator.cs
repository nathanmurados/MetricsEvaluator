using System.IO;
using System.Text;

namespace MetricsUtility.Core.Services
{
    public class SettingsValidator : ISettingsValidator
    {
        public const string Dir = @"C:\MetricsEvaluationUtility\Settings\";
        public const string SettingsFile = "settings.dat";
        public const string SettingsPath = Dir + SettingsFile;

        public const string SettingsHeader = "Specific files to inspect:";
        public const string Ap2SolutionHeader = "AP2 Solution path:";

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
}