namespace MetricsUtility.Core.Services
{
    public interface ISettingsValidator
    {
        void Validate();

        string Dir { get; }
        string SettingsFile { get; }
        string SettingsPath { get; }
        string SettingsHeader { get; }
        string Ap2SolutionHeader { get; }
    }
}