using System;
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

        public MainWindow(IViewModelEvaluator viewModelEvaluator, ISolutionCssMetricsPresenter solutionCssMetricsPresenter, IHumanInterface ux, ISolutionChoicePresenter solutionChoicePresenter)
        {
            SolutionChoicePresenter = solutionChoicePresenter;
            Ux = ux;
            SolutionCssMetricsPresenter = solutionCssMetricsPresenter;
            ViewModelEvaluator = viewModelEvaluator;
            InitializeComponent();
            DataContext = ViewModelEvaluator.Evaluate();

            ux.WriteEvent += (sender, s) => { throw new NotImplementedException(); };
            ux.WriteLineEvent += (sender, s) => { throw new NotImplementedException(); };
            ux.ReadEvent += (sender, s) => { throw new NotImplementedException(); };
            ux.AddOptionEvent += (sender, s) => { throw new NotImplementedException(); };
            ux.DisplayBoolOptionEvent += (sender, s) => { throw new NotImplementedException(); };
            ux.AddOptionEvent += (sender, s) => { throw new NotImplementedException(); };
            ux.AddOptionWithHeadingSpaceEvent += (sender, s) => { throw new NotImplementedException(); };
        }

        private void ChooseSolution(object sender, RoutedEventArgs e)
        {
            SolutionChoicePresenter.Present((ViewModel) DataContext);
        }

        private void ViewSolutionCssMetrics(object sender, RoutedEventArgs e)
        {
            SolutionCssMetricsPresenter.View();
        }
    }
}
