namespace MetricsUtility.Clients.Wpf.Services
{
    public class FilesToInspectStorer : IFilesToInspectStorer
    {
        public void Store(string[] filenames)
        {
            Properties.Settings.Default.LastFiles = string.Join("~", filenames);
            Properties.Settings.Default.Save();
        }
    }
}