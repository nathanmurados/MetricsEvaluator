﻿using System.Windows.Forms;
using MetricsUtility.Clients.Wpf.Services.Evaluators;
using MetricsUtility.Clients.Wpf.Services.Evaluators.Interfaces;
using MetricsUtility.Clients.Wpf.Services.Presenters.Interfaces;
using MetricsUtility.Clients.Wpf.ViewModels;

namespace MetricsUtility.Clients.Wpf.Services.Presenters
{
    public class InspectionPathPresenter : IInspectionPathPresenter
    {
        public IEnableDiagnosticsEvaluator EnableDiagnosticsEvaluator { get; private set; }
        public IPathExistenceEvaluator PathExistenceEvaluator { get; private set; }

        public InspectionPathPresenter(IEnableDiagnosticsEvaluator enableDiagnosticsEvaluator, IPathExistenceEvaluator pathExistenceEvaluator)
        {
            PathExistenceEvaluator = pathExistenceEvaluator;
            EnableDiagnosticsEvaluator = enableDiagnosticsEvaluator;
        }

        public void Present(ViewModel viewModel)
        {
            var dialog = new FolderBrowserDialog
            {
                SelectedPath = Properties.Settings.Default.InspectionPath,
                Description = "Select folder to inspect"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.InspectionPath = dialog.SelectedPath;
                Properties.Settings.Default.Save();
                viewModel.SolutionToAnalyse = Properties.Settings.Default.InspectionPath;
                viewModel.EnableDiagnostics = EnableDiagnosticsEvaluator.Evaluate();
                viewModel.IsValidInspectionDirectory = PathExistenceEvaluator.Evaluate(Properties.Settings.Default.ResultsPath);
            }
        }
    }
}