using System.Collections.Generic;
using System.Linq;
using MetricsUtility.Clients.Wpf.Services.Evaluators.Interfaces;
using MetricsUtility.Clients.Wpf.ViewModels;
using MetricsUtility.Core.Services.Evaluators;

namespace MetricsUtility.Clients.Wpf.Services.Evaluators
{
    public class DirectoryGroupEvaluator : IDirectoryGroupEvaluator
    {
        public IFoldersPerGroupEvaluator FoldersPerGroupEvaluator { get; private set; }
        public IDirectoryDescendentFilesEvaluator DirectoryDescendentFilesEvaluator { get; private set; }

        public DirectoryGroupEvaluator(IFoldersPerGroupEvaluator foldersPerGroupEvaluator, IDirectoryDescendentFilesEvaluator directoryDescendentFilesEvaluator)
        {
            DirectoryDescendentFilesEvaluator = directoryDescendentFilesEvaluator;
            FoldersPerGroupEvaluator = foldersPerGroupEvaluator;
        }

        public List<GroupedFilesViewModel> Evaluate(int numberOfGroups, string[] directories)
        {
            var groups = new List<GroupedFilesViewModel>();
            var foldersPerGroup = FoldersPerGroupEvaluator.Evaluate(directories.Count(), numberOfGroups);

            var i = 0;
            for (var g = 0; g < numberOfGroups; g++)
            {
                var group = new GroupedFilesViewModel { Files = new List<string>() };
                group.StartDir = directories[i];

                for (var f = 0; f < foldersPerGroup; f++)
                {
                    if (i < directories.Count())
                    {
                        group.Files.AddRange(DirectoryDescendentFilesEvaluator.Evaluate(directories[i]).ToList());
                        i++;
                    }
                }

                group.EndDir = directories[i - 1];
                groups.Add(group);
            }

            if (directories.Count() > i)
            {
                for (var r = i; r < directories.Count(); r++)
                {
                    groups.Last().Files.Add(directories[r]);
                    groups.Last().EndDir = directories[r];
                }
            }

            return groups;
        }
    }
}