using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MetricsUtility.Clients.Wpf.Services;
using MetricsUtility.Clients.Wpf.Services.Evaluators;
using MetricsUtility.Clients.Wpf.Services.Evaluators.Interfaces;
using MetricsUtility.Clients.Wpf.Services.Presenters.Interfaces;
using MetricsUtility.Clients.Wpf.ViewModels;
using MetricsUtility.Core.Services;
using MetricsUtility.Core.Services.Evaluators;
using Microsoft.Win32;

namespace MetricsUtility.Clients.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IHasHumanInterface
    {
        public IViewModelEvaluator ViewModelEvaluator { get; private set; }
        public IHumanInterface Ux { get; private set; }
        public ICssMetricsPresenter CssMetricsPresenter { get; private set; }
        public IInspectionPathPresenter InspectionPathPresenter { get; private set; }
        public IResultsPathPresenter ResultsPathPresenter { get; private set; }
        public IBoolOptionPresenter BoolOptionPresenter { get; private set; }
        public IOutputPresenter OutputPresenter { get; private set; }
        public IProgressPresenter ProgressPresenter { get; private set; }
        public IInputPresenter InputPresenter { get; private set; }
        public IOptionsPresenter OptionsPresenter { get; private set; }
        public ISettingsClearer SettingsClearer { get; private set; }
        public IInteractionPermissionToggler InteractionPermissionToggler { get; private set; }
        public IJavaScriptMetricsPresenter JavaScriptMetricsPresenter { get; private set; }
        public IFolderPresenter FolderPresenter { get; private set; }
        public IDirectoryDescendentFilesEvaluator DirectoryDescendentFilesEvaluator { get; private set; }
        public IGroupedCssEvaluator GroupedCssEvaluator { get; private set; }
        public IGroupedJavaScriptEvaluator GroupedJavaScriptEvaluator { get; private set; }
        public IFoldersPerGroupEvaluator FoldersPerGroupEvaluator { get; private set; }
        public IChildDirectoryCountEvaluator ChildDirectoryCountEvaluator { get; private set; }
        public IPathExistenceEvaluator PathExistenceEvaluator { get; private set; }
        public ISpecificGroupEvaluator SpecificGroupEvaluator { get; private set; }
        public IHasFilesToInspectAndIsIdleEvaluator HasFilesToInspectAndIsIdleEvaluator { get; private set; }
        public IFilesToInspectEvaluator FilesToInspectEvaluator { get; private set; }
        public IFilesToInspectStorer FilesToInspectStorer { get; private set; }
        public ICssSpliter CssSpliter { get; private set; }
        public IRefactorPathPresenter RefactorPathPresenter { get; private set; }
        public IGeneratedCssPathPresenter GeneratedCssPathPresenter { get; private set; }
        public ISolutionPathPresenter SolutionPathPresenter { get; private set; }
        public ImageReferencesEvaluator ImageReferencesEvaluator { get; private set; }
        public IJsSplitter JsSplitter { get; private set; }
        
        public MainWindow(IViewModelEvaluator viewModelEvaluator, ICssMetricsPresenter cssMetricsPresenter, IHumanInterface ux, IInspectionPathPresenter inspectionPathPresenter, IResultsPathPresenter resultsPathPresenter, IBoolOptionPresenter boolOptionPresenter, IOutputPresenter outputPresenter, IProgressPresenter progressPresenter, IInputPresenter inputPresenter, IOptionsPresenter optionsPresenter, ISettingsClearer settingsClearer, IInteractionPermissionToggler interactionPermissionToggler, IJavaScriptMetricsPresenter javaScriptMetricsPresenter, IFolderPresenter folderPresenter, IDirectoryDescendentFilesEvaluator directoryDescendentFilesEvaluator, IGroupedCssEvaluator groupedCssEvaluator, IFoldersPerGroupEvaluator foldersPerGroupEvaluator, IChildDirectoryCountEvaluator childDirectoryCountEvaluator, IPathExistenceEvaluator pathExistenceEvaluator, IGroupedJavaScriptEvaluator groupedJavaScriptEvaluator, ISpecificGroupEvaluator specificGroupEvaluator, IHasFilesToInspectAndIsIdleEvaluator hasFilesToInspectAndIsIdleEvaluator, IFilesToInspectEvaluator filesToInspectEvaluator, IFilesToInspectStorer filesToInspectStorer, IRefactorPathPresenter refactorPathPresenter, ICssSpliter cssSpliter, IGeneratedCssPathPresenter generatedCssPathPresenter, ISolutionPathPresenter solutionPathPresenter, ImageReferencesEvaluator imageReferencesEvaluator, IJsSplitter jsSplitter)
        {
            JsSplitter = jsSplitter;
            ImageReferencesEvaluator = imageReferencesEvaluator;
            SolutionPathPresenter = solutionPathPresenter;
            GeneratedCssPathPresenter = generatedCssPathPresenter;
            CssSpliter = cssSpliter;
            RefactorPathPresenter = refactorPathPresenter;
            FilesToInspectStorer = filesToInspectStorer;
            FilesToInspectEvaluator = filesToInspectEvaluator;
            HasFilesToInspectAndIsIdleEvaluator = hasFilesToInspectAndIsIdleEvaluator;
            SpecificGroupEvaluator = specificGroupEvaluator;
            GroupedJavaScriptEvaluator = groupedJavaScriptEvaluator;
            PathExistenceEvaluator = pathExistenceEvaluator;
            ChildDirectoryCountEvaluator = childDirectoryCountEvaluator;
            FoldersPerGroupEvaluator = foldersPerGroupEvaluator;
            GroupedCssEvaluator = groupedCssEvaluator;
            DirectoryDescendentFilesEvaluator = directoryDescendentFilesEvaluator;
            FolderPresenter = folderPresenter;
            JavaScriptMetricsPresenter = javaScriptMetricsPresenter;
            InteractionPermissionToggler = interactionPermissionToggler;
            SettingsClearer = settingsClearer;
            OptionsPresenter = optionsPresenter;
            Ux = ux;
            InputPresenter = inputPresenter;
            OutputPresenter = outputPresenter;
            ProgressPresenter = progressPresenter;
            ViewModelEvaluator = viewModelEvaluator;
            BoolOptionPresenter = boolOptionPresenter;
            InspectionPathPresenter = inspectionPathPresenter;
            CssMetricsPresenter = cssMetricsPresenter;
            ResultsPathPresenter = resultsPathPresenter;

            InitializeComponent();

            ux.ReadEvent += (sender, e) => Application.Current.Dispatcher.BeginInvoke(new Action(() => inputPresenter.Present(sender, e, (ViewModel)DataContext)));
            ux.WriteEvent += (sender, e) => Application.Current.Dispatcher.BeginInvoke(new Action(() => OutputPresenter.Write(sender, e, (ViewModel)DataContext)));
            ux.ProgressEvent += (sender, e) => Application.Current.Dispatcher.BeginInvoke(new Action(() => ProgressPresenter.Present(sender, e, (ViewModel)DataContext)));
            ux.WriteLineEvent += (sender, e) => Application.Current.Dispatcher.BeginInvoke(new Action(() => OutputPresenter.WriteLine(sender, e, (ViewModel)DataContext)));
            ux.AddOptionEvent += (sender, e) => Application.Current.Dispatcher.BeginInvoke(new Action(() => OptionsPresenter.AddOption(sender, e, (ViewModel)DataContext)));
            ux.ResetProgressEvent += (sender, e) => Application.Current.Dispatcher.BeginInvoke(new Action(() => ProgressPresenter.Reset(sender, e, (ViewModel)DataContext)));
            ux.DisplayOptionsEvent += (sender, e) => Application.Current.Dispatcher.BeginInvoke(new Action(() => OptionsPresenter.DisplayOptions(sender, e, (ViewModel)DataContext))); ;
            ux.DisplayBoolOptionEvent += (sender, e) => Application.Current.Dispatcher.BeginInvoke(new Action(() => BoolOptionPresenter.Present(sender, e)));
            ux.AddOptionWithHeadingSpaceEvent += (sender, e) => Application.Current.Dispatcher.BeginInvoke(new Action(() => OptionsPresenter.AddOptionWithHeadingSpace(sender, e, (ViewModel)DataContext)));

            GroupedCssEvaluator.ScrollDown += ScrollDown;
            GroupedJavaScriptEvaluator.ScrollDown += ScrollDown;

            //#if DEBUG
            //            SettingsClearer.Clear();
            //#endif

            DataContext = ViewModelEvaluator.Evaluate();
        }

        private void OpenResultsDirectory(object sender, RoutedEventArgs e)
        {
            FolderPresenter.Present(Properties.Settings.Default.ResultsPath);
        }
        private void OpenInspectionDirectory(object sender, RoutedEventArgs e)
        {
            FolderPresenter.Present(Properties.Settings.Default.InspectionPath);
        }

        private void ChangeResultsPath(object sender, RoutedEventArgs e)
        {
            ResultsPathPresenter.Present((ViewModel)DataContext);
        }
        private void ChangeInspectionPath(object sender, RoutedEventArgs e)
        {
            InspectionPathPresenter.Present((ViewModel)DataContext);
        }
        private void ChangeGeneratedCssPath(object sender, RoutedEventArgs e)
        {
            GeneratedCssPathPresenter.Present((ViewModel)DataContext);
        }
        private void ChangeRefactorPath(object sender, RoutedEventArgs e)
        {
            RefactorPathPresenter.Present((ViewModel)DataContext);
        }
        private void ChangeSolutionRoutePath(object sender, RoutedEventArgs e)
        {
            SolutionPathPresenter.Present((ViewModel)DataContext);
        }

        //private void InspectFileCss(object sender, RoutedEventArgs e)
        //{
        //    DoAction(() =>
        //    {
        //        var dialog = new OpenFileDialog { InitialDirectory = Properties.Settings.Default.InspectionPath, Multiselect = true };
        //        if (dialog.ShowDialog() == true)
        //        {
        //            CssMetricsPresenter.View(dialog.FileNames.ToList());
        //            Properties.Settings.Default.LastFiles = string.Join("~" , dialog.FileNames);
        //            EvaluateRerunButtons();
        //        }
        //    });
        //}
        //private void InspectFileJavaScript(object sender, RoutedEventArgs e)
        //{
        //    DoAction(() =>
        //    {
        //        var dialog = new OpenFileDialog { InitialDirectory = Properties.Settings.Default.InspectionPath, Multiselect = true };
        //        if (dialog.ShowDialog() == true)
        //        {
        //            JavaScriptMetricsPresenter.View(dialog.FileNames.ToList());
        //            Properties.Settings.Default.LastFiles = string.Join("~", dialog.FileNames);
        //            EvaluateRerunButtons();
        //        }
        //    });
        //}
        private void EvaluateRerunButtons()
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                ((ViewModel)DataContext).HasFilesToInspectAndIsIdle = HasFilesToInspectAndIsIdleEvaluator.Evaluate();
            }));
        }
        private void ReRunCss(object sender, RoutedEventArgs e)
        {
            DoAction(() => CssMetricsPresenter.View(FilesToInspectEvaluator.Evaluate()));
        }
        private void ReRunJavaScript(object sender, RoutedEventArgs e)
        {
            DoAction(() => JavaScriptMetricsPresenter.View(FilesToInspectEvaluator.Evaluate()));
        }
        private void ChooseFiles(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog { InitialDirectory = Properties.Settings.Default.InspectionPath, Multiselect = true };
            if (dialog.ShowDialog() == true)
            {
                FilesToInspectStorer.Store(dialog.FileNames);
                EvaluateRerunButtons();
                ((ViewModel)DataContext).FilesToInspect = string.Join(Environment.NewLine, FilesToInspectEvaluator.Evaluate());
            }
        }

        private void InspectFolderCss(object sender, RoutedEventArgs e)
        {
            DoAction(() => CssMetricsPresenter.View(DirectoryDescendentFilesEvaluator.Evaluate(Properties.Settings.Default.InspectionPath).OrderBy(x => x).ToList()));
        }
        private void InspectFolderJavaScript(object sender, RoutedEventArgs e)
        {
            DoAction(() => JavaScriptMetricsPresenter.View(DirectoryDescendentFilesEvaluator.Evaluate(Properties.Settings.Default.InspectionPath).OrderBy(x => x).ToList()));
        }

        private void InspectGroupCss(object sender, RoutedEventArgs e)
        {
            InteractionPermissionToggler.Toggle(false, (ViewModel)DataContext);
            var groupCount = ((ViewModel)DataContext).GroupCount;
            var specificGroup = SpecificGroupEvaluator.Evaluate((ViewModel)DataContext);

            Task.Run(() =>
            {
                var path = Properties.Settings.Default.InspectionPath;
                if (PathExistenceEvaluator.Evaluate(path))
                {
                    GroupedCssEvaluator.Evaluate(groupCount, Directory.GetDirectories(path), specificGroup);
                }
                Application.Current.Dispatcher.BeginInvoke(new Action(() => { InteractionPermissionToggler.Toggle(true, (ViewModel)DataContext); TxtOutput.ScrollToEnd(); }));
            });
        }
        private void InspectGroupJavaScript(object sender, RoutedEventArgs e)
        {
            InteractionPermissionToggler.Toggle(false, (ViewModel)DataContext);
            var groupCount = ((ViewModel)DataContext).GroupCount;
            var specificGroup = SpecificGroupEvaluator.Evaluate((ViewModel)DataContext);

            Task.Run(() =>
            {
                var path = Properties.Settings.Default.InspectionPath;
                if (PathExistenceEvaluator.Evaluate(path))
                {
                    GroupedJavaScriptEvaluator.Evaluate(groupCount, Directory.GetDirectories(path), specificGroup);
                }
                Application.Current.Dispatcher.BeginInvoke(new Action(() => { InteractionPermissionToggler.Toggle(true, (ViewModel)DataContext); TxtOutput.ScrollToEnd(); }));
            });
        }
        private void ChangeNumberOfGroups(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ((ViewModel)DataContext).FoldersPerGroup = FoldersPerGroupEvaluator.Evaluate(ChildDirectoryCountEvaluator.Evaluate(), ((ViewModel)DataContext).GroupCount);
        }

        private void DoAction(Action action)
        {
            InteractionPermissionToggler.Toggle(false, (ViewModel)DataContext);
            Task.Run(() =>
            {
                action();
                Application.Current.Dispatcher.BeginInvoke(new Action(() => { InteractionPermissionToggler.Toggle(true, (ViewModel)DataContext); TxtOutput.ScrollToEnd(); }));
            });
        }

        private void ClearOutput(object sender, RoutedEventArgs e)
        {
            ((ViewModel)DataContext).Output = string.Empty;
        }

        private void ScrollDown(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() => TxtOutput.ScrollToEnd()));
        }

        private void RefactorCss(object sender, RoutedEventArgs e)
        {
            DoAction(() => CssSpliter.Split());
        }

        private void InspectFolderAbsoluteImagePaths(object sender, RoutedEventArgs e)
        {
            ((ViewModel)DataContext).Output = "";
            DoAction(() => ImageReferencesEvaluator.Evaluate());
        }

        private void RefactorJs(object sender, RoutedEventArgs e)
        {
            ((ViewModel)DataContext).Output = "";

            if (MessageBox.Show("Are you sure?", "Refactor JS", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes) { return; }

            DoAction(() =>
            {
                var files = DirectoryDescendentFilesEvaluator.Evaluate(Properties.Settings.Default.RefactorPath).Where(x => x.EndsWith(".cshtml")).ToArray();

                JsSplitter.Split(Properties.Settings.Default.RefactorPath, Properties.Settings.Default.GeneratedFilesPath, files);
            });
        }

        private void RefactorJsWithAtVars(object sender, RoutedEventArgs e)
        {
            ((ViewModel)DataContext).Output = "";

            if (MessageBox.Show("Are you sure?", "Refactor JS", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes) { return; }

            DoAction(() =>
            {
                var files = DirectoryDescendentFilesEvaluator.Evaluate(Properties.Settings.Default.RefactorPath).Where(x => x.EndsWith(".cshtml")).ToArray();

                AdvancedJsSplitter.Split(Properties.Settings.Default.RefactorPath, Properties.Settings.Default.GeneratedFilesPath, files);
            });
        }
    }
}