namespace MetricsUtility.Clients.Wpf.ViewModels
{
    public class ViewModel : ViewModelBase
    {
        private string _solutionToAnalyse;
        private string _output;
        private bool _isIdle;
        private bool _allowFolderChanges;
        private int _progressValue;
        private string _resultsDirectory;
        private bool _isValidResultsDirectory;
        private bool _isValidInspectionDirectory;
        private int _childDirectoryCount;
        private int _groupCount;
        private int _foldersPerGroup;
        private bool _isValidInspectionDirectoryAndIsIdle;

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

        public bool IsIdle
        {
            get { return _isIdle; }
            set
            {
                _isIdle = value; OnPropertyChanged();
                IsValidInspectionDirectoryAndIsIdle = IsValidInspectionDirectory && value;
            }
        }

        public bool AllowFolderChanges
        {
            get { return _allowFolderChanges; }
            set
            {
                _allowFolderChanges = value; 
                OnPropertyChanged();

            }
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
            set
            {
                _isValidInspectionDirectory = value; OnPropertyChanged();
                IsValidInspectionDirectoryAndIsIdle = value && IsIdle;
            }
        }

        public bool IsValidInspectionDirectoryAndIsIdle
        {
            get { return _isValidInspectionDirectoryAndIsIdle; }
            set { _isValidInspectionDirectoryAndIsIdle = value; OnPropertyChanged(); }
        }

        public int ChildDirectoryCount
        {
            get { return _childDirectoryCount; }
            set { _childDirectoryCount = value; OnPropertyChanged(); }
        }

        public int GroupCount
        {
            get { return _groupCount; }
            set { _groupCount = value; OnPropertyChanged(); }
        }

        public int FoldersPerGroup
        {
            get { return _foldersPerGroup; }
            set { _foldersPerGroup = value; OnPropertyChanged(); }
        }
    }
}