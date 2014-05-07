using System.Windows.Forms;
using MetricsUtility.Clients.Wpf.ViewModels;

namespace MetricsUtility.Clients.Wpf.Services.Presenters
{
    public class ResultsDirectoryChoicePresenter : IResultsDirectoryChoicePresenter
    {
        public void Present(ViewModel dataContext)
        {
            var dialog = new FolderBrowserDialog
            {
                SelectedPath = Properties.Settings.Default.ResultsDirectory,
                Description = "Select results directory"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.SolutionToAnalyse = dialog.SelectedPath;
                Properties.Settings.Default.Save();
                dataContext.SolutionToAnalyse = Properties.Settings.Default.ResultsDirectory;
            }
        }
    }
}