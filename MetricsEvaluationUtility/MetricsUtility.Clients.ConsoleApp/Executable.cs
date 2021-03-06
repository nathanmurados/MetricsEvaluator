using System;
using System.Linq;
using MetricsUtility.Core.Services;
using MetricsUtility.Core.Services.Evaluators;
using MetricsUtility.Core.Services.Presenters;
using MetricsUtility.Core.Services.StorageServices;

namespace MetricsUtility.Clients.ConsoleApp
{
    public class Executable
    {
        
        public IHumanInterface Ux { get; private set; }
        public IListPresenter ListPresenter { get; private set; }
        public IFileExtensionPresenter FileExtensionPresenter { get; private set; }
        public IDirectoryDescendentFilesEvaluator DirectoryDescendentFilesEvaluator { get; private set; }
        public IFilteredFilesPresenter FilteredFilesPresenter { get; private set; }
        public ICssStatsPresenter CssStatsPresenter { get; private set; }
        public IFilteredFilesStatsPresenter FilteredFilesStatsPresenter { get; private set; }
        public IJavaScriptStatsPresenter JavaScriptStatsPresenter { get; private set; }
        public IFilteredFilesEvaluator FilteredFilesEvaluator { get; private set; }
        public IJavaScriptFileStatsPresenter JavaScriptFileStatsPresenter { get; private set; }
        public ISettingsValidator SettingsValidator { get; private set; }
        public ISettingsEvaluator SettingsEvaluator { get; private set; }
        public IJavaScriptStatsStorageService JavaScriptStatsStorageService { get; private set; }
        public ICssStatsStorageService CssStatsStorageService { get; private set; }

        public Executable(IHumanInterface ux, IFileExtensionPresenter fileExtensionPresenter, IDirectoryDescendentFilesEvaluator directoryDescendentFilesEvaluator, IListPresenter listPresenter, IFilteredFilesPresenter filteredFilesPresenter, IFilteredFilesStatsPresenter filteredFilesStatsPresenter, ICssStatsPresenter cssStatsPresenter, IJavaScriptStatsPresenter javaScriptStatsPresenter, IFilteredFilesEvaluator filteredFilesEvaluator, IJavaScriptFileStatsPresenter javaScriptFileStatsPresenter, ISettingsValidator settingsValidator, ISettingsEvaluator settingsEvaluator, IJavaScriptStatsStorageService javaScriptStatsStorageService, ICssStatsStorageService cssStatsStorageService)
        {
            CssStatsStorageService = cssStatsStorageService;
            JavaScriptStatsStorageService = javaScriptStatsStorageService;
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
            DirectoryDescendentFilesEvaluator = directoryDescendentFilesEvaluator;
            FileExtensionPresenter = fileExtensionPresenter;
            Ux = ux;
        }

        public void Execute()
        {
            SettingsValidator.Validate();

            var directory = SettingsEvaluator.GetApTwoDirectory();

            var files = DirectoryDescendentFilesEvaluator.Evaluate(directory).OrderBy(x => x).ToList();

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

                Ux.AddOptionWithHeadingSpace("Count inline CSS on filtered files", () =>
                {
                    var results = CssStatsPresenter.Present(FilteredFilesEvaluator.Evaluate(files));
                    Ux.DisplayBoolOption("Store detailed CSS results to disk?", () => CssStatsStorageService.Store(results, string.Empty), null);
                });

                Ux.AddOptionWithHeadingSpace("Count inline Javascript on filtered files", () =>
                {
                    var results = JavaScriptStatsPresenter.Present(FilteredFilesEvaluator.Evaluate(files));
                    Ux.DisplayBoolOption("Store detailed JavaScript results to disk?", () => JavaScriptStatsStorageService.Store(results, string.Empty), null);
                });

                Ux.AddOptionWithHeadingSpace("Count inline Javascript and CSS on specific file...", () => JavaScriptFileStatsPresenter.Present());

                Ux.DisplayOptions("Please choose an option");
            }
        }
    }
}