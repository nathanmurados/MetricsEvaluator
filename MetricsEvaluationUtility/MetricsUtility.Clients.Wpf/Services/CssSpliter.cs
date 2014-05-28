using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using MetricsUtility.Core.Services;
using MetricsUtility.Core.Services.RefactorServices;
using MetricsUtility.Core.ViewModels;

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

        public void Split(bool mergeBlocks)
        {
            if (MessageBox.Show("Are you sure?", "Refactor CSS", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes) { return; }

            var refactorTarget = Properties.Settings.Default.RefactorPath;

            var newPath = GetNewPath();

            if (!Proceed(refactorTarget)) { return; }

            var filesToRefactor = Directory.GetFiles(refactorTarget).Where(x => x.EndsWith(".cshtml"));

            foreach (var file in filesToRefactor)
            {
                Ux.WriteLine(string.Format("Refactoring {0}", file));

                SeperatedCssViewModel seperatedCssViewModel;

                try
                {
                    seperatedCssViewModel = PageCssSeperationEvaluator.Evaluate(File.ReadAllLines(file), Properties.Settings.Default.SolutionPath, newPath, file,mergeBlocks);
                }
                catch (Exception)
                {
                    var x = string.Format("---ERROR: Unable to process {0}---", file);
                    Ux.WriteLine(x);
                    Ux.WriteLine("");
                    MessageBox.Show(x, "OPERATION HALTED", MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }


                if (seperatedCssViewModel.ExtractedCssBlocks.Any())
                {
                    foreach (var newCssFile in seperatedCssViewModel.ExtractedCssBlocks)
                    {
                        var url = newPath + "\\" + newCssFile.ProposedFileName;

                        if (!Directory.Exists(newPath))
                        {
                            Directory.CreateDirectory(newPath);
                        }

                        if (File.Exists(url))
                        {
                            MessageBox.Show("PLEASE REVIEW THIS FILE MANUALLY - " + url);
                            Ux.WriteLine("ERROR - TASK STOPPED");
                            Ux.WriteLine("");
                            return;
                        }

                        File.WriteAllLines(url, newCssFile.Lines);

                        var atSigns = newCssFile.Lines.Count(x => x.Contains("@"));
                        var dotDotSlashes = newCssFile.Lines.Count(x => x.Contains("../"));
                        if (atSigns > 0) Ux.WriteLine(string.Format("---WARNING: {0} lines containing @ were detected", atSigns));
                        if (dotDotSlashes > 0) Ux.WriteLine(string.Format("---WARNING: {0} lines containing ../ were detected", dotDotSlashes));
                    }
                    Ux.WriteLine("Created " + seperatedCssViewModel.ExtractedCssBlocks.Count() + " new files");
                    File.WriteAllLines(file, seperatedCssViewModel.StripedContent);
                }
            }

            Ux.WriteLine("Operation complete.");
            Ux.WriteLine("");
        }

        private static string GetNewPath()
        {
            var newFolder = Properties.Settings.Default.RefactorPath.Split('\\').Last();
            var newPath = Properties.Settings.Default.GeneratedFilesPath + "\\" + newFolder;
            return newPath;
        }

        private static bool Proceed(string refactorTarget)
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