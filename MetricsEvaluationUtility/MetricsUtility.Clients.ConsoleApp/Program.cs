using MetricsUtility.Clients.ConsoleApp.Services;
using MetricsUtility.Core.Services;
using MetricsUtility.Core.Services.Evaluators;
using MetricsUtility.Core.Services.Evaluators.Css;
using MetricsUtility.Core.Services.Evaluators.JavaScript;
using MetricsUtility.Core.Services.Presenters;
using MetricsUtility.Core.Services.StorageServices;
using Ninject;
using Ninject.Modules;

namespace MetricsUtility.Clients.ConsoleApp
{
    class Program
    {
        class IoCModule : NinjectModule
        {
            public override void Load()
            {
                Bind<IHumanInterface>().To<ConsoleAppHumanInterface>();
                Bind<IFileExtensionPresenter>().To<FileExtensionsPresenter>();
                Bind<IDirectoryDescendentFilesEvaluator>().To<DirectoryDescendentFilesEvaluator>();
                Bind<IListPresenter>().To<ListPresenter>();
                Bind<IFilteredFilesPresenter>().To<FilteredFilesPresenter>();
                Bind<IFilteredFilesEvaluator>().To<FilteredFilesEvaluator>();
                Bind<IFilteredFilesStatsPresenter>().To<FilteredFilesStatsPresenter>();
                Bind<ICssStatsPresenter>().To<CssStatsPresenter>();
                Bind<ICssValidationEvaluator>().To<CssValidationEvaluator>();
                Bind<ICssStatsStorageService>().To<CssStatsStorageService>();
                Bind<IDateTimeProvider>().To<DateTimeProvider>();
                Bind<ICssBlockEvaluator>().To<CssBlockEvaluator>();
                Bind<ICssRazorEvaluator>().To<CssRazorEvaluator>();
                Bind<ICssBlockContentEvaluator>().To<CssBlockContentEvaluator>();
                Bind<IJavaScriptStatsPresenter>().To<JavaScriptStatsPresenter>();
                Bind<IJsValidationEvaluator>().To<JsValidationEvaluator>();
                Bind<IJsBlockEvaluator>().To<JsBlockEvaluator>();
                Bind<IJsBlockContentEvaluator>().To<JsBlockContentEvaluator>();
                Bind<IJsRazorEvaluator>().To<JsRazorEvaluator>();
                Bind<IJsReferencesEvaluator>().To<JsReferencesEvaluator>();
                Bind<IJavaScriptStatsStorageService>().To<JavaScriptStatsStorageService>();
                Bind<IRelevantAttributesEvaluator>().To<RelevantAttributesEvaluator>();
                Bind<IStorageService>().To<StorageService>();
                Bind<IJavaScriptFileStatsPresenter>().To<JavaScriptFileStatsPresenter>();
                Bind<ISettingsValidator>().To<SettingsValidator>();
                Bind<ISettingsEvaluator>().To<SettingsEvaluator>();
                Bind<IResultsDirectoryEvaluator>().To<ResultsDirectoryEvaluator>();
                Bind<ICssStatsFileNameEvaluator>().To<CssStatsFileNameEvaluator>();
                Bind<IJavaScriptStatsFileNameEvaluator>().To<JavaScriptStatsFileNameEvaluator>();
            }
        }

        static void Main(string[] args)
        {
            IKernel kernel = new StandardKernel(new IoCModule());

            var prog = new Executable(
                kernel.Get<IHumanInterface>(),
                kernel.Get<IFileExtensionPresenter>(),
                kernel.Get<IDirectoryDescendentFilesEvaluator>(),
                kernel.Get<IListPresenter>(),
                kernel.Get<IFilteredFilesPresenter>(),
                kernel.Get<IFilteredFilesStatsPresenter>(),
                kernel.Get<ICssStatsPresenter>(),
                kernel.Get<IJavaScriptStatsPresenter>(),
                kernel.Get<IFilteredFilesEvaluator>(),
                kernel.Get<JavaScriptFileStatsPresenter>(),
                kernel.Get<ISettingsValidator>(),
                kernel.Get<ISettingsEvaluator>(),
                kernel.Get<IJavaScriptStatsStorageService>(),
                kernel.Get<ICssStatsStorageService>()
            );

            prog.Execute();
        }
    }
}
