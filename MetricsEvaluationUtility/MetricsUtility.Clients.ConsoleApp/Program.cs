using MetricsUtility.Clients.ConsoleApp.Services;
using MetricsUtility.Core.Services;
using MetricsUtility.Core.Services.Evaluators;
using MetricsUtility.Core.Services.Evaluators.Css;
using MetricsUtility.Core.Services.Evaluators.JavaScript;
using MetricsUtility.Core.Services.Presenters;
using MetricsUtility.Core.Services.Storers;
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
                Bind<IDirectoryFileEvaluator>().To<DirectoryFileEvaluator>();
                Bind<IListPresenter>().To<ListPresenter>();
                Bind<IFilteredFilesPresenter>().To<FilteredFilesPresenter>();
                Bind<IFilteredFilesEvaluator>().To<FilteredFilesEvaluator>();
                Bind<IFilteredFilesStatsPresenter>().To<FilteredFilesStatsPresenter>();
                Bind<ICssStatsPresenter>().To<CssStatsPresenter>();
                Bind<ICssValidationEvaluator>().To<CssValidationEvaluator>();
                Bind<ICssStatsStorer>().To<CssStatsStorer>();
                Bind<IDateTimeProvider>().To<DateTimeProvider>();
                Bind<ICssBlockEvaluator>().To<CssBlockEvaluator>();
                Bind<ICssRazorEvaluator>().To<CssRazorEvaluator>();
                Bind<ICssPageEvaluator>().To<CssPageEvaluator>();
                Bind<IJavaScriptStatsPresenter>().To<JavaScriptStatsPresenter>();
                Bind<IJsValidationEvaluator>().To<JsValidationEvaluator>();
                Bind<IJsBlockEvaluator>().To<JsBlockEvaluator>();
                Bind<IJsPageEvaluator>().To<JsPageEvaluator>();
                Bind<IJsRazorEvaluator>().To<JsRazorEvaluator>();
                Bind<IJsReferencesEvaluator>().To<JsReferencesEvaluator>();
                Bind<IJavaScriptStatsStorer>().To<JavaScriptStatsStorer>();
                Bind<IRelevantAttributesEvaluator>().To<RelevantAttributesEvaluator>();
                Bind<IStorer>().To<Storer>();
                Bind<IJavaScriptFileStatsPresenter>().To<JavaScriptFileStatsPresenter>();
                Bind<ISettingsValidator>().To<SettingsValidator>();
                Bind<ISettingsEvaluator>().To<SettingsEvaluator>();
                Bind<IResultsDirectoryEvaluator>().To<ResultsDirectoryEvaluator>();
                Bind<ICssStatsFileNameEvaluator>().To<CssStatsFileNameEvaluator>();
            }
        }

        static void Main(string[] args)
        {
            IKernel kernel = new StandardKernel(new IoCModule());

            var prog = new Executable(
                kernel.Get<IHumanInterface>(),
                kernel.Get<IFileExtensionPresenter>(),
                kernel.Get<IDirectoryFileEvaluator>(),
                kernel.Get<IListPresenter>(),
                kernel.Get<IFilteredFilesPresenter>(),
                kernel.Get<IFilteredFilesStatsPresenter>(),
                kernel.Get<ICssStatsPresenter>(),
                kernel.Get<IJavaScriptStatsPresenter>(),
                kernel.Get<IFilteredFilesEvaluator>(),
                kernel.Get<JavaScriptFileStatsPresenter>(),
                kernel.Get<ISettingsValidator>(),
                kernel.Get<ISettingsEvaluator>(),
                kernel.Get<IJavaScriptStatsStorer>(),
                kernel.Get<ICssStatsStorer>()
            );

            prog.Execute();
        }
    }
}
