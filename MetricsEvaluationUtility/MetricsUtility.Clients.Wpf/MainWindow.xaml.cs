using System;
using System.Windows;
using System.Windows.Forms;
using MetricsUtility.Clients.Wpf.Services.Evaluators;
using MetricsUtility.Clients.Wpf.ViewModels;
using MetricsUtility.Core.Services;
using MetricsUtility.Core.Services.Evaluators;
using MetricsUtility.Core.Services.Presenters;
using MetricsUtility.Core.Services.Storers;

namespace MetricsUtility.Clients.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window,IHasHumanInterface
    {
        public IViewModelEvaluator ViewModelEvaluator { get; private set; }
        public ICssStatsPresenter CssStatsPresenter { get; private set; }
        public IFilteredFilesEvaluator FilteredFilesEvaluator { get; private set; }
        public ICssStatsStorer CssStatsStorer { get; private set; }
        public IHumanInterface Ux { get; private set; }

        public MainWindow(IViewModelEvaluator viewModelEvaluator, ICssStatsPresenter cssStatsPresenter, IFilteredFilesEvaluator filteredFilesEvaluator, ICssStatsStorer cssStatsStorer, IHumanInterface ux)
        {
            Ux = ux;
            CssStatsStorer = cssStatsStorer;
            FilteredFilesEvaluator = filteredFilesEvaluator;
            CssStatsPresenter = cssStatsPresenter;
            ViewModelEvaluator = viewModelEvaluator;
            InitializeComponent();
            DataContext = ViewModelEvaluator.Evaluate();
        }

        private void ChooseSolution(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog { SelectedPath = Properties.Settings.Default.SolutionToAnalyse };

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Properties.Settings.Default.SolutionToAnalyse = dialog.SelectedPath;
                Properties.Settings.Default.Save();
                ((ViewModel) DataContext).SolutionToAnalyse = Properties.Settings.Default.SolutionToAnalyse;
            }
        }

        private void ViewSolutionCssMetrics(object sender, RoutedEventArgs e)
        {
            var results = CssStatsPresenter.Present(FilteredFilesEvaluator.Evaluate(files));
            Ux.DisplayBoolOption("Store detailed CSS results to disk?", () => CssStatsStorer.Store(results), null);
        }

        public class WpfInterface : IHumanInterface
        {
            public void Write(string str)
            {
                ((ViewModel) DataContext).Output += str;
            }

            public void WriteLine(string str)
            {
                throw new NotImplementedException();
            }

            public string Read(string str)
            {
                throw new NotImplementedException();
            }

            public void AddOption(string title, Action action)
            {
                throw new NotImplementedException();
            }

            public void DisplayOptions(string question)
            {
                throw new NotImplementedException();
            }

            public void AddOptionWithHeadingSpace(string title, Action action)
            {
                throw new NotImplementedException();
            }

            public void DisplayBoolOption(string question, Action actionOnTrue, Action actionOnFalse)
            {
                throw new NotImplementedException();
            }
        }

    }
}
