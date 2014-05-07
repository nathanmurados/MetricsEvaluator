namespace MetricsUtility.Clients.Wpf.ViewModels
{
    public class ViewModel : ViewModelBase
    {
        private string _solutionToAnalyse;
        private string _output;

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
    }
}