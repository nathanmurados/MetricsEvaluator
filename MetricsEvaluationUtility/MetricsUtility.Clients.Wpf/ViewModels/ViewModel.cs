using System.Windows;

namespace MetricsUtility.Clients.Wpf.ViewModels
{
    public class ViewModel : ViewModelBase
    {
        private string _solutionToAnalyse;
        private string _output;
        private Visibility _enableDiagnostics;
        private bool _allowInteractions;
        private int _progressValue;
        private string _resultsDirectory;

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

        public bool AllowInteractions
        {
            get { return _allowInteractions; }
            set { _allowInteractions = value; OnPropertyChanged(); }
        }

        public int ProgressValue
        {
            get { return _progressValue; }
            set { _progressValue = value; OnPropertyChanged(); }
        }

        public string ResultsDirectory
        {
            get { return _resultsDirectory; }
            set { _resultsDirectory = value; OnPropertyChanged(); }
        }
    }
}