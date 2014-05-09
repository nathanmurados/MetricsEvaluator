using MetricsUtility.Clients.Wpf.Services;
using MetricsUtility.Clients.Wpf.Services.Evaluators;
using MetricsUtility.Clients.Wpf.Services.Evaluators.Interfaces;
using MetricsUtility.Clients.Wpf.Services.Presenters;
using MetricsUtility.Clients.Wpf.Services.Presenters.Interfaces;
using MetricsUtility.Core.Services;
using MetricsUtility.Core.Services.Evaluators;
using MetricsUtility.Core.Services.Evaluators.Css;
using MetricsUtility.Core.Services.Evaluators.JavaScript;
using MetricsUtility.Core.Services.Presenters;
using MetricsUtility.Core.Services.Refactorers;
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
            _container.Bind<IHumanInterface>().To<WpfInterface>().InSingletonScope();
            _container.Bind<IFileExtensionPresenter>().To<FileExtensionsPresenter>();
            _container.Bind<IDirectoryDescendentFilesEvaluator>().To<DirectoryDescendentFilesEvaluator>();
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
            _container.Bind<ICssMetricsPresenter>().To<CssMetricsPresenter>();
            _container.Bind<IInspectionPathPresenter>().To<InspectionPathPresenter>();
            _container.Bind<IResultsDirectoryEvaluator>().To<ResultsDirectoryEvaluator>();
            _container.Bind<IResultsPathPresenter>().To<ResultsPathPresenter>();

            _container.Bind<IBoolOptionPresenter>().To<BoolOptionPresenter>();
            _container.Bind<IOutputPresenter>().To<OutputPresenter>();
            _container.Bind<IProgressPresenter>().To<ProgressPresenter>();
            _container.Bind<IInputPresenter>().To<InputPresenter>();
            _container.Bind<IEnableDiagnosticsEvaluator>().To<EnableDiagnosticsEvaluator>();
            _container.Bind<IOptionsPresenter>().To<OptionsPresenter>();
            _container.Bind<ISettingsClearer>().To<SettingsClearer>();
            _container.Bind<IInteractionPermissionToggler>().To<InteractionPermissionToggler>();
            _container.Bind<IJavaScriptMetricsPresenter>().To<JavaScriptMetricsPresenter>();
            _container.Bind<IFolderPresenter>().To<FolderPresenter>();
            _container.Bind<IPathExistenceEvaluator>().To<PathExistenceEvaluator>();
            _container.Bind<ICssStatsFileNameEvaluator>().To<CssStatsFileNameEvaluator>();
            _container.Bind<IFilePresenter>().To<FilePresenter>();
            _container.Bind<IFileExistenceEvaluator>().To<FileExistenceEvaluator>();
            _container.Bind<IJavaScriptStatsFileNameEvaluator>().To<JavaScriptStatsFileNameEvaluator>();
            _container.Bind<IChildDirectoryCountEvaluator>().To<ChildDirectoryCountEvaluator>();
            _container.Bind<IGroupedCssEvaluator>().To<GroupedCssEvaluator>().InSingletonScope();
            _container.Bind<IFoldersPerGroupEvaluator>().To<FoldersPerGroupEvaluator>();
            _container.Bind<IDirectoryGroupEvaluator>().To<DirectoryGroupEvaluator>();
            _container.Bind<IGroupedJavaScriptEvaluator>().To<GroupedJavaScriptEvaluator>();
            _container.Bind<IEnableGroupingEvaluator>().To<EnableGroupingEvaluator>();
            _container.Bind<ISpecificGroupEvaluator>().To<SpecificGroupEvaluator>();
            _container.Bind<IHasFilesToInspectAndIsIdleEvaluator>().To<HasFilesToInspectAndIsIdleEvaluator>();
            _container.Bind<IFilesToInspectEvaluator>().To<FilesToInspectEvaluator>();
            _container.Bind<IFilesToInspectStorer>().To<FilesToInspectStorer>();
            _container.Bind<ICssRefactorPathPresenter>().To<CssRefactorPathPresenter>();
            _container.Bind<ICssSpliter>().To<CssSpliter>();
            _container.Bind<IPageCssSeperationEvaluator>().To<PageCssSeperationEvaluator>();
            _container.Bind<IHasCssRefactorPathsEvaluator>().To<HasCssRefactorPathsEvaluator>();
            _container.Bind<IGeneratedCssPathPresenter>().To<GeneratedCssPathPresenter>();
            //_container.Bind<IJavaScriptStatsFileNameEvaluator>().To<JavaScriptStatsFileNameEvaluator>();
            //_container.Bind<IJavaScriptStatsFileNameEvaluator>().To<JavaScriptStatsFileNameEvaluator>();
            //_container.Bind<IJavaScriptStatsFileNameEvaluator>().To<JavaScriptStatsFileNameEvaluator>();
            //_container.Bind<IJavaScriptStatsFileNameEvaluator>().To<JavaScriptStatsFileNameEvaluator>();
            //_container.Bind<IJavaScriptStatsFileNameEvaluator>().To<JavaScriptStatsFileNameEvaluator>();
        
        }

        private void ComposeObjects()
        {
            Current.MainWindow = _container.Get<MainWindow>();
            Current.MainWindow.Title = "Metrics Evaluation Utility";
        }
    }
}
