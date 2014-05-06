using System;
using System.Linq;
using System.Net.Sockets;
using MetricsEvaluationUtility.Services;
using MetricsEvaluationUtility.Services.Evaluators;
using MetricsEvaluationUtility.Services.Presenters;

namespace MetricsEvaluationUtility
{
    public class Executable
    {
        
        public IHumanInterface Ux { get; private set; }
        public IListPresenter ListPresenter { get; private set; }
        public IFileExtensionPresenter FileExtensionPresenter { get; private set; }
        public IDirectoryFileEvaluator DirectoryFileEvaluator { get; private set; }
        public IFilteredFilesPresenter FilteredFilesPresenter { get; private set; }
        public ICssStatsPresenter CssStatsPresenter { get; private set; }
        public IFilteredFilesStatsPresenter FilteredFilesStatsPresenter { get; private set; }
        public IJavaScriptStatsPresenter JavaScriptStatsPresenter { get; private set; }
        public IFilteredFilesEvaluator FilteredFilesEvaluator { get; private set; }
        public IJavaScriptFileStatsPresenter JavaScriptFileStatsPresenter { get; private set; }
        public ISettingsValidator SettingsValidator { get; private set; }
        public ISettingsEvaluator SettingsEvaluator { get; private set; }
        
        public Executable(IHumanInterface ux, IFileExtensionPresenter fileExtensionPresenter, IDirectoryFileEvaluator directoryFileEvaluator, IListPresenter listPresenter, IFilteredFilesPresenter filteredFilesPresenter, IFilteredFilesStatsPresenter filteredFilesStatsPresenter, ICssStatsPresenter cssStatsPresenter, IJavaScriptStatsPresenter javaScriptStatsPresenter, IFilteredFilesEvaluator filteredFilesEvaluator, IJavaScriptFileStatsPresenter javaScriptFileStatsPresenter, ISettingsValidator settingsValidator, ISettingsEvaluator settingsEvaluator)
        {
            SettingsEvaluator = settingsEvaluator;

            SettingsValidator = settingsValidator;
            JavaScriptFileStatsPresenter = javaScriptFileStatsPresenter;
            FilteredFilesEvaluator = filteredFilesEvaluator;
            JavaScriptStatsPresenter = javaScriptStatsPresenter;
            CssStatsPresenter = cssStatsPresenter;
            FilteredFilesStatsPresenter = filteredFilesStatsPresenter;
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);

            FilteredFilesPresenter = filteredFilesPresenter;
            ListPresenter = listPresenter;
            DirectoryFileEvaluator = directoryFileEvaluator;
            FileExtensionPresenter = fileExtensionPresenter;
            Ux = ux;
        }

        public void Execute()
        {
            SettingsValidator.Validate();

            var directory = SettingsEvaluator.GetApTwoDirectory();

            var files = DirectoryFileEvaluator.GetFiles(directory).OrderBy(x => x).ToList();

            var loop = true;
            while (loop)
            {
                Ux.WriteLine(string.Format("Analysing: {0}", directory));

                Ux.AddOption("Exit", () => loop = false);

                Ux.AddOptionWithHeadingSpace("View all file extensions", () => FileExtensionPresenter.Present(files));
                Ux.AddOption("View all files", () => ListPresenter.Present(files));
                Ux.AddOption("View filtered extensions", () => FilteredFilesPresenter.PresentFilteredExtensions());
                Ux.AddOption("View filtered files", () => FilteredFilesPresenter.PresentFilteredFiles(files));
                Ux.AddOption("View filtered file statistics", () => FilteredFilesStatsPresenter.Present(files));

                Ux.AddOptionWithHeadingSpace("Count inline CSS on filtered files", () => CssStatsPresenter.Present(FilteredFilesEvaluator.Evaluate(files)));

                Ux.AddOptionWithHeadingSpace("Count inline Javascript on filtered files", () => JavaScriptStatsPresenter.Present(FilteredFilesEvaluator.Evaluate(files)));

                Ux.AddOptionWithHeadingSpace("Count inline Javascript on specific file...", () => JavaScriptFileStatsPresenter.Present());

                Ux.DisplayOptions("Please choose an option");
            }
        }
    }
}