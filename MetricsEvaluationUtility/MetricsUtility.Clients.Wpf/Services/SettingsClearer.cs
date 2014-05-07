namespace MetricsUtility.Clients.Wpf.Services
{
    public class SettingsClearer : ISettingsClearer
    {
        public void Clear()
        {
            Properties.Settings.Default.ResultsPath = null;
            Properties.Settings.Default.InspectionPath = null;
        }
    }
}