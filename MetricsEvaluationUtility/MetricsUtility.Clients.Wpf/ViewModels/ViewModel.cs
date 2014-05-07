namespace MetricsUtility.Clients.Wpf.ViewModels
{
    public class ViewModel : ViewModelBase
    {
        private string _solutionToAnalyse;
        private string _output;
        private bool _enableDiagnostics;
        private bool _allowInteractions;
        private int _progressValue;
        private string _resultsDirectory;
        private bool _isValidResultsDirectory;
        private bool _isValidInspectionDirectory;

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

        public bool EnableDiagnostics
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

        public bool IsValidResultsDirectory
        {
            get { return _isValidResultsDirectory; }
            set { _isValidResultsDirectory = value; OnPropertyChanged(); }
        }

        public bool IsValidInspectionDirectory
        {
            get { return _isValidInspectionDirectory; }
            set { _isValidInspectionDirectory = value; OnPropertyChanged(); }
        }
    }
}