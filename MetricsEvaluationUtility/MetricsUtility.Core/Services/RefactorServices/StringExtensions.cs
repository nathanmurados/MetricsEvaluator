namespace MetricsUtility.Core.Services.RefactorServices
{
    public static class StringExtensions
    {
        public static string Remove(this string str, string toRemove)
        {
            return toRemove.Length == 0 ? str : str.Replace(toRemove, "");
        }
    }
}