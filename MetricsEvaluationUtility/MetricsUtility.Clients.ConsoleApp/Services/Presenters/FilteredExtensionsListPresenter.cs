using System.Collections.Generic;
using MetricsEvaluationUtility.Services.Evaluators;

namespace MetricsEvaluationUtility.Services.Presenters
{
    public class FilteredFilesPresenter : IFilteredFilesPresenter
    {
        public IFilteredFilesEvaluator FilteredFilesEvaluator { get; private set; }
        public IListPresenter ListPresenter { get; private set; }

        public FilteredFilesPresenter(IListPresenter listPresenter, IFilteredFilesEvaluator filteredFilesEvaluator)
        {
            FilteredFilesEvaluator = filteredFilesEvaluator;
            ListPresenter = listPresenter;
        }

        public void PresentFilteredFiles(IEnumerable<string> files)
        {
            ListPresenter.Present(FilteredFilesEvaluator.Evaluate(files));
        }

        public void PresentFilteredExtensions()
        {
            ListPresenter.Present(FilteredFilesEvaluator.EvaluateFilteredExtensions());
        }
    }

    public interface IFilteredFilesPresenter : IHasListPresenter
    {
        void PresentFilteredFiles(IEnumerable<string> files);
        void PresentFilteredExtensions();
    }
}