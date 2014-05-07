using MetricsEvaluationUtility.Services.Presenters;

namespace MetricsEvaluationUtility.Services
{
    public interface IHasListPresenter
    {
        IListPresenter ListPresenter { get; }
    }
}