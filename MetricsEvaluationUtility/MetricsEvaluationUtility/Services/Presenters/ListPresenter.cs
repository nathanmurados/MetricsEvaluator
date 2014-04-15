using System.Collections.Generic;
using System.Linq;

namespace MetricsEvaluationUtility.Services.Presenters
{
    public interface IListPresenter
    {
        void Present(IEnumerable<string> items);
    }

    public class ListPresenter : IListPresenter
    {
        public IHumanInterface Ux { get; private set; }

        public ListPresenter(IHumanInterface ux)
        {
            Ux = ux;
        }


        public void Present(IEnumerable<string> items)
        {
            foreach (var file in items)
            {
                Ux.WriteLine(file);
            }

            Ux.WriteLine("Total: " + items.Count());
        }
    }
}