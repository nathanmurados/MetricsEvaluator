using System;
using System.Threading.Tasks;
using System.Windows;
using MetricsUtility.Clients.Wpf.Services.Evaluators;
using MetricsUtility.Clients.Wpf.Services.Presenters;
using MetricsUtility.Clients.Wpf.ViewModels;
using MetricsUtility.Core.Services;

namespace MetricsUtility.Clients.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IHasHumanInterface
    {
        public IViewModelEvaluator ViewModelEvaluator { get; private set; }
        public IHumanInterface Ux { get; private set; }
        public ISolutionCssMetricsPresenter SolutionCssMetricsPresenter { get; private set; }
        public ISolutionChoicePresenter SolutionChoicePresenter { get; private set; }
        public IResultsDirectoryChoicePresenter ResultsDirectoryChoicePresenter { get; private set; }
        public IBoolOptionPresenter BoolOptionPresenter { get; private set; }
        public IOutputPresenter OutputPresenter { get; private set; }
        public IProgressPresenter ProgressPresenter { get; private set; }
        public IInputPresenter InputPresenter { get; private set; }
        public IOptionsPresenter OptionsPresenter { get; set; }

        public MainWindow(IViewModelEvaluator viewModelEvaluator, ISolutionCssMetricsPresenter solutionCssMetricsPresenter, IHumanInterface ux, ISolutionChoicePresenter solutionChoicePresenter, IResultsDirectoryChoicePresenter resultsDirectoryChoicePresenter, IBoolOptionPresenter boolOptionPresenter, IOutputPresenter outputPresenter, IProgressPresenter progressPresenter, IInputPresenter inputPresenter)
        {
            InputPresenter = inputPresenter;
            ProgressPresenter = progressPresenter;
            OutputPresenter = outputPresenter;
            BoolOptionPresenter = boolOptionPresenter;
            ResultsDirectoryChoicePresenter = resultsDirectoryChoicePresenter;
            SolutionChoicePresenter = solutionChoicePresenter;
            Ux = ux;
            SolutionCssMetricsPresenter = solutionCssMetricsPresenter;
            ViewModelEvaluator = viewModelEvaluator;
            InitializeComponent();
            DataContext = ViewModelEvaluator.Evaluate();

            ux.ReadEvent += (sender, e) => Application.Current.Dispatcher.BeginInvoke(new Action(() => inputPresenter.Present(sender, e, (ViewModel)DataContext)));
            ux.WriteEvent += (sender, e) => Application.Current.Dispatcher.BeginInvoke(new Action(() => OutputPresenter.Write(sender, e, (ViewModel)DataContext)));
            ux.ProgressEvent += (sender, e) => Application.Current.Dispatcher.BeginInvoke(new Action(() => ProgressPresenter.Present(sender, e, (ViewModel)DataContext)));
            ux.WriteLineEvent += (sender, e) => Application.Current.Dispatcher.BeginInvoke(new Action(() => OutputPresenter.WriteLine(sender, e, (ViewModel)DataContext)));
            ux.AddOptionEvent += (sender, e) => Application.Current.Dispatcher.BeginInvoke(new Action(() => OptionsPresenter.AddOption(sender, e, (ViewModel)DataContext)));
            ux.ResetProgressEvent += (sender, e) => Application.Current.Dispatcher.BeginInvoke(new Action(() => ProgressPresenter.Reset(sender, e, (ViewModel)DataContext)));
            ux.DisplayOptionsEvent += (sender, e) => Application.Current.Dispatcher.BeginInvoke(new Action(() => OptionsPresenter.DisplayOptions(sender, e, (ViewModel)DataContext))); ;
            ux.DisplayBoolOptionEvent += (sender, e) => Application.Current.Dispatcher.BeginInvoke(new Action(() => BoolOptionPresenter.Present(sender, e)));
            ux.AddOptionWithHeadingSpaceEvent += (sender, e) => Application.Current.Dispatcher.BeginInvoke(new Action(() => OptionsPresenter.AddOptionWithHeadingSpace(sender, e, (ViewModel)DataContext)));

#if DEBUG
            ClearSettings();
#endif
        }

        private static void ClearSettings()
        {
            Properties.Settings.Default.ResultsPath = null;
            Properties.Settings.Default.InspectionPath = null;
        }

        private void ChooseSolution(object sender, RoutedEventArgs e)
        {
            SolutionChoicePresenter.Present((ViewModel)DataContext);
        }

        private void ViewSolutionCssMetrics(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                ToggleInteractionPermission(false);
                SolutionCssMetricsPresenter.View();
                ToggleInteractionPermission(true);
            });
        }

        private void ToggleInteractionPermission(bool allow)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                ((ViewModel)DataContext).AllowInteractions = allow;
            }));
        }

        private void ChooseResultsLocation(object sender, RoutedEventArgs e)
        {
            ResultsDirectoryChoicePresenter.Present((ViewModel)DataContext);
        }
    }
}
