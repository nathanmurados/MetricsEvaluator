namespace MetricsUtility.Clients.Wpf.Services
{
    public interface IDirectoryMimicker
    {
        string Mimick(string refactorPath, string generatedFilesPath, string file);
    }
}