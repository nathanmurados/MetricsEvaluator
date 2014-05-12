using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using MetricsUtility.Core.Services;
using MetricsUtility.Core.Services.Refactorers;

namespace MetricsUtility.Clients.Wpf.Services
{
    public class CssSpliter : ICssSpliter, IHasHumanInterface
    {
        public IPageCssSeperationEvaluator PageCssSeperationEvaluator { get; private set; }
        public IHumanInterface Ux { get; private set; }

        public CssSpliter(IPageCssSeperationEvaluator pageCssSeperationEvaluator, IHumanInterface ux)
        {
            Ux = ux;
            PageCssSeperationEvaluator = pageCssSeperationEvaluator;
        }

        public void Split()
        {
            var refactorTarget = Properties.Settings.Default.RefactorCssPath;

            if (!Proceed(refactorTarget)) { return; }

            var filesToRefactor = Directory.GetFiles(refactorTarget);

            foreach (var file in filesToRefactor)
            {
                Ux.WriteLine(string.Format("Refactoring {0}", file));

                var seperatedCssViewModel = PageCssSeperationEvaluator.Evaluate(File.ReadAllLines(file), Properties.Settings.Default.SolutionPath, Properties.Settings.Default.GeneratedCssPath, file);
                var parts = Path.GetFileName(file).Split('.');
                var newCssName = string.Format("{0}.css", string.Join(".", parts.Take(parts.Length - 1)));

                if (seperatedCssViewModel.ExtractedCssContent.Any())
                {
                    File.WriteAllLines(Properties.Settings.Default.GeneratedCssPath + "\\" + newCssName, seperatedCssViewModel.ExtractedCssContent);
                }
                File.WriteAllLines(file, seperatedCssViewModel.UpdatedContent);
            }
        }

        private bool Proceed(string refactorTarget)
        {
            var dirCount = Directory.GetDirectories(refactorTarget).Count();
            if (dirCount > 0)
            {
                var sb = new StringBuilder();
                sb.AppendLine("This refactor operation will only be applied to files immedietly in your inspection directory.");
                sb.AppendLine("");
                sb.AppendLine(string.Format("Any files within the {0} sub-folder(s) will NOT be refactored.", dirCount));
                var result = MessageBox.Show(sb.ToString(), "WARNING", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (result != MessageBoxResult.OK)
                {
                    return false;
                }
            }
            return true;
        }
    }
}