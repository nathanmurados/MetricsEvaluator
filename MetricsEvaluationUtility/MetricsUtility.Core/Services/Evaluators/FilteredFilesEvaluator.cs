using System;
using System.Collections.Generic;
using System.Linq;

namespace MetricsUtility.Core.Services.Evaluators
{
    public interface IValidExtensionsEvaluator
    {
        List<string> Evaluate();
    }

    public class ValidExtensionsEvaluator : IValidExtensionsEvaluator
    {
        public List<string> Evaluate()
        {
            return new List<string>{"asax","asmx","asp","aspx","cs","cshtml",
                                    //"css",
                                    "htm",
                                    "html",
                                    //"js",
                                    "master",
                                   };
        }
    }

    public class FilteredFilesEvaluator : IFilteredFilesEvaluator
    {
        public IValidExtensionsEvaluator ValidExtensionsEvaluator { get; private set; }
        public FilteredFilesEvaluator(IValidExtensionsEvaluator validExtensionsEvaluator)
        {
            ValidExtensionsEvaluator = validExtensionsEvaluator;
        }


        public List<string> Evaluate(IEnumerable<string> files)
        {
            return files.ToList().Where(filename => ValidExtensionsEvaluator.Evaluate().Any(extension => filename.EndsWith(extension, StringComparison.OrdinalIgnoreCase))).ToList();
        }


        public List<string> EvaluateFilteredExtensions()
        {
            return ValidExtensionsEvaluator.Evaluate();
        }
    }
}