using System;
using System.Collections.Generic;
using System.Linq;

namespace MetricsEvaluationUtility.Services.Evaluators
{
    public interface IFilteredFilesEvaluator
    {
        List<string> Evaluate(IEnumerable<string> files);
        List<string> EvaluateFilteredExtensions();
    }
    
    public class FilteredFilesEvaluator : IFilteredFilesEvaluator
    {
        private static readonly List<string> FilteredExtensions = new List<string>
        {
            "asax",
            "asmx",
            "asp",
            "aspx",
            "cs",
            "cshtml",
            //"css",
            "htm",
            "html",
            //"js",
            "master",
        };

        public List<string> Evaluate(IEnumerable<string> files)
        {
            return files.ToList().Where(filename => FilteredExtensions.Any(extension => filename.EndsWith(extension, StringComparison.OrdinalIgnoreCase))).ToList();
        }


        public List<string> EvaluateFilteredExtensions()
        {
            return FilteredExtensions;
        }
    }
}