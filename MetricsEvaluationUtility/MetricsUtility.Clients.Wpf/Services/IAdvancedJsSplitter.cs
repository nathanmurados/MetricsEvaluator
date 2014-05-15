namespace MetricsUtility.Clients.Wpf.Services
{
    public interface IAdvancedJsSplitter
    {
        void Split(string refactorPath, string generatedFilesPath, string[] filesToRefactor);
    }
}