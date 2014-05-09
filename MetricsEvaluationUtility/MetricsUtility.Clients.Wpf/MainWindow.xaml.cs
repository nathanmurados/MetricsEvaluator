﻿using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MetricsUtility.Clients.Wpf.Services;
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
        public IHasLastFilesAndIsIdleEvaluator HasLastFilesAndIsIdleEvaluator { get; private set; }

        public MainWindow(IViewModelEvaluator viewModelEvaluator, ICssMetricsPresenter cssMetricsPresenter, IHumanInterface ux, IInspectionPathPresenter inspectionPathPresenter, IResultsPathPresenter resultsPathPresenter, IBoolOptionPresenter boolOptionPresenter, IOutputPresenter outputPresenter, IProgressPresenter progressPresenter, IInputPresenter inputPresenter, IOptionsPresenter optionsPresenter, ISettingsClearer settingsClearer, IInteractionPermissionToggler interactionPermissionToggler, IJavaScriptMetricsPresenter javaScriptMetricsPresenter, IFolderPresenter folderPresenter, IDirectoryDescendentFilesEvaluator directoryDescendentFilesEvaluator, IGroupedCssEvaluator groupedCssEvaluator, IFoldersPerGroupEvaluator foldersPerGroupEvaluator, IChildDirectoryCountEvaluator childDirectoryCountEvaluator, IPathExistenceEvaluator pathExistenceEvaluator, IGroupedJavaScriptEvaluator groupedJavaScriptEvaluator, ISpecificGroupEvaluator specificGroupEvaluator, IHasLastFilesAndIsIdleEvaluator hasLastFilesAndIsIdleEvaluator)
        {
            HasLastFilesAndIsIdleEvaluator = hasLastFilesAndIsIdleEvaluator;
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

        private void ChangeResultsDirectory(object sender, RoutedEventArgs e)
        {
            ResultsPathPresenter.Present((ViewModel)DataContext);
        }

        private void ChangeInspectionDirectory(object sender, RoutedEventArgs e)
        {
            InspectionPathPresenter.Present((ViewModel)DataContext);
        }

        private void InspectFileCss(object sender, RoutedEventArgs e)
        {
            DoAction(() =>
            {
                var dialog = new OpenFileDialog { InitialDirectory = Properties.Settings.Default.InspectionPath, Multiselect = true };
                if (dialog.ShowDialog() == true)
                {
                    CssMetricsPresenter.View(dialog.FileNames.ToList());
                    Properties.Settings.Default.LastFiles = string.Join("~" , dialog.FileNames);
                    EvaluateRerunButtons();
                }
            });
        }

        private void InspectFileJavaScript(object sender, RoutedEventArgs e)
        {
            DoAction(() =>
            {
                var dialog = new OpenFileDialog { InitialDirectory = Properties.Settings.Default.InspectionPath, Multiselect = true };
                if (dialog.ShowDialog() == true)
                {
                    JavaScriptMetricsPresenter.View(dialog.FileNames.ToList());
                    Properties.Settings.Default.LastFiles = string.Join("~", dialog.FileNames);
                    EvaluateRerunButtons();
                }
            });
        }

        private void EvaluateRerunButtons()
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                ((ViewModel)DataContext).HasLastFilesAndIsIdle = HasLastFilesAndIsIdleEvaluator.Evaluate();
            }));
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

        private void DoAction(Action action)
        {
            InteractionPermissionToggler.Toggle(false, (ViewModel)DataContext);
            Task.Run(() =>
            {
                action();
                Application.Current.Dispatcher.BeginInvoke(new Action(() => { InteractionPermissionToggler.Toggle(true, (ViewModel)DataContext); TxtOutput.ScrollToEnd(); }));
            });
        }

        private void ChangeNumberOfGroups(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ((ViewModel)DataContext).FoldersPerGroup = FoldersPerGroupEvaluator.Evaluate(ChildDirectoryCountEvaluator.Evaluate(), ((ViewModel)DataContext).GroupCount);
        }

        private void ClearOutput(object sender, RoutedEventArgs e)
        {
            ((ViewModel) DataContext).Output = string.Empty;
        }

        private void ScrollDown(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() => TxtOutput.ScrollToEnd()));
        }

        private void ReRunCss(object sender, RoutedEventArgs e)
        {
            DoAction(() => CssMetricsPresenter.View(Properties.Settings.Default.LastFiles.Split('~').ToList()));
        }

        private void ReRunJavaScript(object sender, RoutedEventArgs e)
        {
            DoAction(() => JavaScriptMetricsPresenter.View(Properties.Settings.Default.LastFiles.Split('~').ToList()));
        }

        private void RefactorCss(object sender, RoutedEventArgs e)
        {
            DoAction(()=> );
        }
    }
}