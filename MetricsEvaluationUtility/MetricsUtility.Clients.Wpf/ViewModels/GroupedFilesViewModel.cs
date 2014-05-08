using System.Collections.Generic;

namespace MetricsUtility.Clients.Wpf.ViewModels
{
    public class GroupedFilesViewModel
    {
        public string StartDir { get; set; }
        public string EndDir { get; set; }
        public List<string> Files { get; set; }
    }
}