using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using MetricsUtility.Core.Services;
using MetricsUtility.Core.Services.Refactorers;
using MetricsUtility.Core.ViewModels;
using MessageBox = System.Windows.MessageBox;

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
            if (MessageBox.Show("Are you sure?", "Refactor CSS", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes) { return; }

            var refactorTarget = Properties.Settings.Default.RefactorCssPath;

            var newPath = GetNewPath();

            if (!Proceed(refactorTarget)) { return; }

            var filesToRefactor = Directory.GetFiles(refactorTarget).Where(x => x.EndsWith(".cshtml"));

            foreach (var file in filesToRefactor)
            {
                Ux.WriteLine(string.Format("Refactoring {0}", file));

                SeperatedCssViewModel seperatedCssViewModel ;
                
                try
                {
                    seperatedCssViewModel = PageCssSeperationEvaluator.Evaluate(File.ReadAllLines(file), Properties.Settings.Default.SolutionPath, newPath, file);
                }
                catch (Exception)
                {
                    MessageBox.Show("Unable to process " + file);
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
                            return;
                        }

                        File.WriteAllLines(url, newCssFile.Lines);
                    }
                    Ux.WriteLine("Created " + seperatedCssViewModel.ExtractedCssBlocks.Count() + " new files");
                }
                File.WriteAllLines(file, seperatedCssViewModel.StripedContent);
            }

           Ux.WriteLine("Operation complete.");
        }

        private string GetNewPath()
        {
            var newFolder = Properties.Settings.Default.RefactorCssPath.Split('\\').Last();
            var newPath = Properties.Settings.Default.GeneratedCssPath + "\\" + newFolder;
            return newPath;
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