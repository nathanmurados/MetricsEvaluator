using System.Collections.Generic;
using System.IO;

namespace MetricsEvaluationUtility.Services.Evaluators
{
    public class SettingsEvaluator : ISettingsEvaluator
    {
        public string GetApTwoDirectory()
        {
            var lines = File.ReadAllLines(SettingsValidator.SettingsPath);
            return lines[1];
        }


        public List<string> GetSpecificFiles()
        {
            var lines = File.ReadAllLines(SettingsValidator.SettingsPath);

            var get = false;

            var files = new List<string>();

            foreach (var line in lines)
            {
                if (get)
                {
                    files.Add(line);
                }
                if (line == SettingsValidator.Ap2SolutionHeader)
                {
                    get = true;
                }
            }
            return files;
        }
    }
}