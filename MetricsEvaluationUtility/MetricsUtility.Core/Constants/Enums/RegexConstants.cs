namespace MetricsUtility.Core.Constants.Enums
{
    public class RegexConstants
    {
        // We want to ignore commented out script tags
        // (?<!@\\*) - check that @* does not exist before <script
        //
        public static string ScriptOpeningTag = "(?<!@\\*)<script(?=\\s|>)(?!(?:[^>=]|=(['\"])(?:(?!\\1).)*\\1)*?\\ssrc=['\"])[^>]*>";
        public static string StyleOpeningTag = "<style[^>]+type\\s*=\\s*['\"]([^'\"]+)['\"][^>]*>";
        public static string ScriptClosingTag = "</script>";
        public static string StyleClosingTag = "</style>";
    }
}