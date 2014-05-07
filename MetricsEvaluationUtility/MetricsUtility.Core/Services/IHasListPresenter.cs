using MetricsUtility.Core.Services.Presenters;

namespace MetricsUtility.Core.Services
{
    public interface IHasListPresenter
    {
        IListPresenter ListPresenter { get; }
    }
}