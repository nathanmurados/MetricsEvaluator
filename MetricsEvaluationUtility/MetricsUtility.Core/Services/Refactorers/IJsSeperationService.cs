﻿namespace MetricsUtility.Core.Services.Refactorers
{
    public interface IJsSeperationService
    {
        SeperatedJs Evaluate(string[] readAllLines, string solutionPath, string generatedResultDirectory, string file);
    }
}