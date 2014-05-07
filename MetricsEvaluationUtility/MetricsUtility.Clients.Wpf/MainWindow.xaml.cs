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

        public MainWindow(IViewModelEvaluator viewModelEvaluator, ISolutionCssMetricsPresenter solutionCssMetricsPresenter, IHumanInterface ux, ISolutionChoicePresenter solutionChoicePresenter)
        {
            SolutionChoicePresenter = solutionChoicePresenter;
            Ux = ux;
            SolutionCssMetricsPresenter = solutionCssMetricsPresenter;
            ViewModelEvaluator = viewModelEvaluator;
            InitializeComponent();
            DataContext = ViewModelEvaluator.Evaluate();

            ux.WriteEvent += Write;
            ux.WriteLineEvent += WriteLineEvent;
            ux.ReadEvent += ReadEvent;
            ux.AddOptionEvent += AddOptionEvent;
            ux.DisplayBoolOptionEvent += DisplayBoolOptionEvent;
            ux.AddOptionEvent += AddOptionEvent;
            ux.AddOptionWithHeadingSpaceEvent += AddOptionWithHeadingSpaceEvent;
            ux.DisplayOptionsEvent += DisplayOptionsEvent;
            ux.ProgressEvent += ProgressEvent;
            ux.ResetProgressEvent += ResetProgressEvent;
        }

        private void ResetProgressEvent(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                ((ViewModel)DataContext).ProgressValue = 0;
            }));
        }

        private void ProgressEvent(object sender, int e)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                ((ViewModel) DataContext).ProgressValue = e;
            }));
        }

        private void DisplayOptionsEvent(object sender, string e)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                MessageBox.Show("options");
            }));
        }

        private void AddOptionWithHeadingSpaceEvent(object sender, AddOptionEventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {

            }));
        }

        private void DisplayBoolOptionEvent(object sender, BoolOptionEventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() => MessageBox.Show("Yes no")));
        }

        private void AddOptionEvent(object sender, AddOptionEventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {

            }));
        }

        private void ReadEvent(object sender, string e)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                MessageBox.Show("Read");
            }));
        }

        private void WriteLineEvent(object sender, string e)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                ((ViewModel)DataContext).Output += string.Format("{0}{1}", e, Environment.NewLine);
            }));
        }

        public void Write(object sender, string e)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                ((ViewModel)DataContext).Output += e;
            }));
        }

        private void ChooseSolution(object sender, RoutedEventArgs e)
        {
            SolutionChoicePresenter.Present((ViewModel)DataContext);
        }

        private void ViewSolutionCssMetrics(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                AllowInteractions(false);
                SolutionCssMetricsPresenter.View();
                AllowInteractions(true);
            });
        }

        private void AllowInteractions(bool allow)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                ((ViewModel)DataContext).AllowInteractions = allow;
            }));
        }
    }
}
