using System.IO;
using System.Text;

namespace MetricsUtility.Core.Services.Storers
{
    public sealed class Storer : IStorer
    {
        public IHumanInterface Ux { get; private set; }
        public IDateTimeProvider DateTimeProvider { get; private set; }

        public Storer(IHumanInterface ux, IDateTimeProvider dateTimeProvider)
        {
            DateTimeProvider = dateTimeProvider;
            Ux = ux;
        }

        private const string Root = @"C:\MetricsEvaluationUtility\";

        public void Store(StringBuilder stringBuilder, string fileName)
        {
            if (!Directory.Exists(Root))
            {
                Directory.CreateDirectory(Root);
            }

            File.WriteAllText(Root + fileName, stringBuilder.ToString());
        }
    }

    public interface IStorer : IHasHumanInterface, IHasDateTimeProvider
    {
        void Store(StringBuilder stringBuilder, string fileName);
    }
}