﻿using System.Windows.Forms;
using MetricsUtility.Clients.Wpf.Services.Evaluators.Interfaces;
using MetricsUtility.Clients.Wpf.Services.Presenters.Interfaces;
using MetricsUtility.Clients.Wpf.ViewModels;

namespace MetricsUtility.Clients.Wpf.Services.Presenters
{
    public class CssRefactorPathPresenter : ICssRefactorPathPresenter
    {
        public IHasCssRefactorPathsEvaluator HasCssRefactorPathsEvaluator { get; private set; }
        public IEnableDiagnosticsEvaluator EnableDiagnosticsEvaluator { get; private set; }

        public CssRefactorPathPresenter(IHasCssRefactorPathsEvaluator hasCssRefactorPathsEvaluator, IEnableDiagnosticsEvaluator enableDiagnosticsEvaluator)
        {
            EnableDiagnosticsEvaluator = enableDiagnosticsEvaluator;
            HasCssRefactorPathsEvaluator = hasCssRefactorPathsEvaluator;
        }


        public void Present(ViewModel viewModel)
        {
            var dialog = new FolderBrowserDialog
            {
                SelectedPath = Properties.Settings.Default.RefactorCssPath,
                Description = "Select the target CSS folder to refactor"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.RefactorCssPath = dialog.SelectedPath;
                Properties.Settings.Default.Save();
                viewModel.RefactorCssDirectory = Properties.Settings.Default.RefactorCssPath;
                viewModel.HasCssRefactorPaths = HasCssRefactorPathsEvaluator.Evaluate();
                viewModel.IsIdle = EnableDiagnosticsEvaluator.Evaluate();
            }
        }
    }
}