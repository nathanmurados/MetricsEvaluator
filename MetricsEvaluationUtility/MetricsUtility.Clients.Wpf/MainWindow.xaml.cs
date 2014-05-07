using System;
using System.Threading.Tasks;
using System.Windows;
using MetricsUtility.Clients.Wpf.Services;
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
        public ISolutionPathPresenter SolutionPathPresenter { get; private set; }
        public IResultsPathPresenter ResultsPathPresenter { get; private set; }
        public IBoolOptionPresenter BoolOptionPresenter { get; private set; }
        public IOutputPresenter OutputPresenter { get; private set; }
        public IProgressPresenter ProgressPresenter { get; private set; }
        public IInputPresenter InputPresenter { get; private set; }
        public IOptionsPresenter OptionsPresenter { get; private set; }
        public ISettingsClearer SettingsClearer { get; private set; }
        public IInteractionPermissionToggler InteractionPermissionToggler { get; private set; }
        public ISolutionJavaScriptMetricsPresenter SolutionJavaScriptMetricsPresenter { get; private set; }

        public MainWindow(IViewModelEvaluator viewModelEvaluator, ISolutionCssMetricsPresenter solutionCssMetricsPresenter, IHumanInterface ux, ISolutionPathPresenter solutionPathPresenter, IResultsPathPresenter resultsPathPresenter, IBoolOptionPresenter boolOptionPresenter, IOutputPresenter outputPresenter, IProgressPresenter progressPresenter, IInputPresenter inputPresenter, IOptionsPresenter optionsPresenter, ISettingsClearer settingsClearer, IInteractionPermissionToggler interactionPermissionToggler, ISolutionJavaScriptMetricsPresenter solutionJavaScriptMetricsPresenter)
        {
            SolutionJavaScriptMetricsPresenter = solutionJavaScriptMetricsPresenter;
            InteractionPermissionToggler = interactionPermissionToggler;
            SettingsClearer = settingsClearer;
            OptionsPresenter = optionsPresenter;
            Ux = ux;
            InputPresenter = inputPresenter;
            OutputPresenter = outputPresenter;
            ProgressPresenter = progressPresenter;
            ViewModelEvaluator = viewModelEvaluator;
            BoolOptionPresenter = boolOptionPresenter;
            SolutionPathPresenter = solutionPathPresenter;
            SolutionCssMetricsPresenter = solutionCssMetricsPresenter;
            ResultsPathPresenter = resultsPathPresenter;

            InitializeComponent();

            ux.ReadEvent += (sender, e) => Application.Current.Dispatcher.BeginInvoke(new Action(() => inputPresenter.Present(sender, e, (ViewModel)DataContext)));
            ux.WriteEvent += (sender, e) => Application.Current.Dispatcher.BeginInvoke(new Action(() => OutputPresenter.Write(sender, e, (ViewModel)DataContext)));
            ux.ProgressEvent += (sender, e) => Application.Current.Dispatcher.BeginInvoke(new Action(() => ProgressPresenter.Present(sender, e, (ViewModel)DataContext)));
            ux.WriteLineEvent += (sender, e) => Application.Current.Dispatcher.BeginInvoke(new Action(() => OutputPresenter.WriteLine(sender, e, (ViewModel)DataContext)));
            ux.AddOptionEvent += (sender, e) => Application.Current.Dispatcher.BeginInvoke(new Action(() => OptionsPresenter.AddOption(sender, e, (ViewModel)DataContext)));
            ux.ResetProgressEvent += (sender, e) => Application.Current.Dispatcher.BeginInvoke(new Action(() => ProgressPresenter.Reset(sender, e, (ViewModel)DataContext)));
            ux.DisplayOptionsEvent += (sender, e) => Application.Current.Dispatcher.BeginInvoke(new Action(() => OptionsPresenter.DisplayOptions(sender, e, (ViewModel)DataContext))); ;
            ux.DisplayBoolOptionEvent += (sender, e) => Application.Current.Dispatcher.BeginInvoke(new Action(() => BoolOptionPresenter.Present(sender, e)));
            ux.AddOptionWithHeadingSpaceEvent += (sender, e) => Application.Current.Dispatcher.BeginInvoke(new Action(() => OptionsPresenter.AddOptionWithHeadingSpace(sender, e, (ViewModel)DataContext)));

            DataContext = ViewModelEvaluator.Evaluate();
        }

        private void ChooseSolution(object sender, RoutedEventArgs e)
        {
            SolutionPathPresenter.Present((ViewModel)DataContext);
        }

        private void ViewSolutionCssMetrics(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                Application.Current.Dispatcher.BeginInvoke(new Action(() => InteractionPermissionToggler.Toggle(false, (ViewModel) DataContext)));

                SolutionCssMetricsPresenter.View();

                Application.Current.Dispatcher.BeginInvoke(new Action(() => InteractionPermissionToggler.Toggle(true, (ViewModel)DataContext)));
            });
        }

        private void ChooseResultsLocation(object sender, RoutedEventArgs e)
        {
            ResultsPathPresenter.Present((ViewModel)DataContext);
        }

        private void ViewSolutionJavaScriptMetrics(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                Application.Current.Dispatcher.BeginInvoke(new Action(() => InteractionPermissionToggler.Toggle(false, (ViewModel)DataContext)));

                SolutionJavaScriptMetricsPresenter.View();

                Application.Current.Dispatcher.BeginInvoke(new Action(() => InteractionPermissionToggler.Toggle(true, (ViewModel)DataContext)));
            });
        }
    }
}
