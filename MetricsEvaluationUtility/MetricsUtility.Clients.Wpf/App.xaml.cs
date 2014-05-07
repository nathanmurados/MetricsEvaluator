using MetricsUtility.Clients.Wpf.Services.Evaluators;
using MetricsUtility.Clients.Wpf.ViewModels;
using MetricsUtility.Core.Services;
using MetricsUtility.Core.Services.Evaluators;
using MetricsUtility.Core.Services.Evaluators.Css;
using MetricsUtility.Core.Services.Evaluators.JavaScript;
using MetricsUtility.Core.Services.Presenters;
using MetricsUtility.Core.Services.Storers;
using Ninject;
using System.Windows;

namespace MetricsUtility.Clients.Wpf
{

    public partial class App
    {
        private IKernel _container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureContainer();
            ComposeObjects();
            Current.MainWindow.Show();
        }

        private void ConfigureContainer()
        {
            _container = new StandardKernel();
            //_container.Bind<IHumanInterface>().To<ConsoleAppHumanInterface>();
            _container.Bind<IFileExtensionPresenter>().To<FileExtensionsPresenter>();
            _container.Bind<IDirectoryFileEvaluator>().To<DirectoryFileEvaluator>();
            _container.Bind<IListPresenter>().To<ListPresenter>();
            _container.Bind<IFilteredFilesPresenter>().To<FilteredFilesPresenter>();
            _container.Bind<IFilteredFilesEvaluator>().To<FilteredFilesEvaluator>();
            _container.Bind<IFilteredFilesStatsPresenter>().To<FilteredFilesStatsPresenter>();
            _container.Bind<ICssStatsPresenter>().To<CssStatsPresenter>();
            _container.Bind<ICssValidationEvaluator>().To<CssValidationEvaluator>();
            _container.Bind<ICssStatsStorer>().To<CssStatsStorer>();
            _container.Bind<IDateTimeProvider>().To<DateTimeProvider>();
            _container.Bind<ICssBlockEvaluator>().To<CssBlockEvaluator>();
            _container.Bind<ICssRazorEvaluator>().To<CssRazorEvaluator>();
            _container.Bind<ICssPageEvaluator>().To<CssPageEvaluator>();
            _container.Bind<IJavaScriptStatsPresenter>().To<JavaScriptStatsPresenter>();
            _container.Bind<IJsValidationEvaluator>().To<JsValidationEvaluator>();
            _container.Bind<IJsBlockEvaluator>().To<JsBlockEvaluator>();
            _container.Bind<IJsPageEvaluator>().To<JsPageEvaluator>();
            _container.Bind<IJsRazorEvaluator>().To<JsRazorEvaluator>();
            _container.Bind<IJsReferencesEvaluator>().To<JsReferencesEvaluator>();
            _container.Bind<IJavaScriptStatsStorer>().To<JavaScriptStatsStorer>();
            _container.Bind<IRelevantAttributesEvaluator>().To<RelevantAttributesEvaluator>();
            _container.Bind<IStorer>().To<Storer>();
            _container.Bind<IJavaScriptFileStatsPresenter>().To<JavaScriptFileStatsPresenter>();
            //_container.Bind<ISettingsValidator>().To<SettingsValidator>();
            _container.Bind<ISettingsEvaluator>().To<SettingsEvaluator>();
            _container.Bind<IViewModelEvaluator>().To<ViewModelEvaluator>();
        }

        private void ComposeObjects()
        {
            Current.MainWindow = _container.Get<MainWindow>();
            Current.MainWindow.Title = "DI with Ninject";
        }
    }
}
