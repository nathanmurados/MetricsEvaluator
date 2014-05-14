namespace MetricsUtility.Clients.Wpf.Services
{
    public interface IJsSplitter
    {
        void Split(string refactorPath, string generatedFilesPath, string[] filesToRefactor);
    }
}