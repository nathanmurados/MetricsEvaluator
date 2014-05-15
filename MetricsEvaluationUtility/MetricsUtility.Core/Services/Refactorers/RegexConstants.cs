namespace MetricsUtility.Core.Services.Refactorers
{
    public class RegexConstants
    {
        public static string ScriptOpeningTag = "<script(?=\\s|>)(?!(?:[^>=]|=(['\"])(?:(?!\\1).)*\\1)*?\\ssrc=['\"])[^>]*>";
        public static string StyleOpeningTag = "<style[^>]+type\\s*=\\s*['\"]([^'\"]+)['\"][^>]*>";
        public static string ScriptClosingTag = "</script>";
        public static string StyleClosingTag = "</style>";
    }
}