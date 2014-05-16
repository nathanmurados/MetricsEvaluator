using System.Text;

namespace MetricsUtility.Core.Services.StorageServices
{
    public interface IStorageService
    {
        string Store(StringBuilder stringBuilder, string fileName);
    }
}