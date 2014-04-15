using System;
using System.Collections.Generic;
using System.Linq;
using MetricsEvaluationUtility.Services.Evaluators;

namespace MetricsEvaluationUtility.Services.Presenters
{
    public class FilteredFilesStatsPresenter : IFilteredFilesStatsPresenter
    {
        public IHumanInterface Ux { get; private set; }
        public IFilteredFilesEvaluator FilteredFilesEvaluator { get; private set; }

        public FilteredFilesStatsPresenter(IFilteredFilesEvaluator filteredFilesEvaluator, IHumanInterface ux)
        {
            Ux = ux;
            FilteredFilesEvaluator = filteredFilesEvaluator;
        }


        public void Present(IEnumerable<string> files)
        {
            var filteredFiles = FilteredFilesEvaluator.Evaluate(files);

            foreach (var filteredExtension in FilteredFilesEvaluator.EvaluateFilteredExtensions())
            {
                Console.WriteLine("# of {0} files: {1}", filteredExtension, filteredFiles.Count(file => file.EndsWith(filteredExtension, StringComparison.OrdinalIgnoreCase)));
            }
        }
    }

    public interface IFilteredFilesStatsPresenter : IPresenter
    {
        void Present(IEnumerable<string> files);
    }
}