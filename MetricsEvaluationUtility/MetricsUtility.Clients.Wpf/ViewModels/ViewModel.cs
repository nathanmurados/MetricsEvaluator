using System.Windows;

namespace MetricsUtility.Clients.Wpf.ViewModels
{
    public class ViewModel : ViewModelBase
    {
        private string _solutionToAnalyse;
        private string _output;
        private Visibility _enableDiagnostics;

        public string SolutionToAnalyse
        {
            get { return _solutionToAnalyse; }
            set { _solutionToAnalyse = value; OnPropertyChanged(); }
        }

        public string Output
        {
            get { return _output; }
            set { _output = value; OnPropertyChanged(); }
        }

        public Visibility EnableDiagnostics
        {
            get { return _enableDiagnostics; }
            set { _enableDiagnostics = value; OnPropertyChanged(); }
        }
    }
}