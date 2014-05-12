namespace MetricsUtility.Clients.Wpf.ViewModels
{
    public class ViewModel : ViewModelBase
    {
        private string _inspectionDirectory;
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
        private bool _enableGroupSelecting;
        private bool? _enableSpecificGroup;
        private int _specificGroupToInspect;
        private bool _hasFilesToInspectAndIsIdle;
        private string _filesToInspect;
        private string _refactorCssDirectory;
        private bool _hasCssRefactorPaths;
        private string _generatedCssDirectory;
        private string _solutionDirectory;
        
        public string InspectionDirectory
        {
            get { return _inspectionDirectory; }
            set { _inspectionDirectory = value; OnPropertyChanged(); }
        }

        public string Output
        {
            get { return _output; }
            set { _output = value; OnPropertyChanged(); }
        }

        public bool IsIdle
        {
            get { return _isIdle; }
            set { _isIdle = value; OnPropertyChanged(); }
        }

        public bool AllowFolderChanges
        {
            get { return _allowFolderChanges; }
            set { _allowFolderChanges = value; OnPropertyChanged(); }
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

        public bool EnableGroupSelecting
        {
            get { return _enableGroupSelecting; }
            set { _enableGroupSelecting = value; OnPropertyChanged(); }
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

        public bool? EnableSpecificGroup
        {
            get { return _enableSpecificGroup; }
            set
            {
                _enableSpecificGroup = value; OnPropertyChanged();
            }
        }

        public int SpecificGroupToInspect
        {
            get { return _specificGroupToInspect; }
            set { _specificGroupToInspect = value; OnPropertyChanged(); }
        }

        public bool HasFilesToInspectAndIsIdle
        {
            get { return _hasFilesToInspectAndIsIdle; }
            set { _hasFilesToInspectAndIsIdle = value; OnPropertyChanged(); }
        }

        public string FilesToInspect
        {
            get { return _filesToInspect; }
            set { _filesToInspect = value; OnPropertyChanged(); }
        }

        public string RefactorCssDirectory
        {
            get { return _refactorCssDirectory; }
            set { _refactorCssDirectory = value; OnPropertyChanged(); }
        }

        public bool HasCssRefactorPaths
        {
            get { return _hasCssRefactorPaths; }
            set { _hasCssRefactorPaths = value; OnPropertyChanged(); }
        }

        public string GeneratedCssDirectory
        {
            get { return _generatedCssDirectory; }
            set { _generatedCssDirectory = value; OnPropertyChanged(); }
        }

        public string SolutionDirectory
        {
            get { return _solutionDirectory; }
            set { _solutionDirectory = value; OnPropertyChanged(); }
        }
    }
}