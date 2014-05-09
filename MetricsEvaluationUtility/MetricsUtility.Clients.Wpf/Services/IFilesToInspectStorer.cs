namespace MetricsUtility.Clients.Wpf.Services
{
    public interface IFilesToInspectStorer
    {
        void Store(string[] filenames);
    }
}