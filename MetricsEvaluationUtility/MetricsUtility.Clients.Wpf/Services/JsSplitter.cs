using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using MetricsUtility.Core.Services;
using MetricsUtility.Core.Services.Refactorers;

namespace MetricsUtility.Clients.Wpf.Services
{

    public class DirectoryMimicker : IDirectoryMimicker
    {
        public string Mimick(string refactorPath, string generatedFilesPath, string file)
        {
            if (refactorPath.EndsWith("\\")) throw new NotImplementedException();

            var bit = file.Replace(refactorPath, "");

            var parts = file.Split('\\');
            var fileName = parts.Last();

            var newPath = bit.Replace(fileName, "");

            var newPathParts = newPath.Split('\\');
            newPath = string.Join("\\", newPathParts.Take(newPathParts.Length - 1));

            return string.Format("{0}{1}", generatedFilesPath, newPath);
        }
    }

    public interface IDirectoryMimicker
    {
        string Mimick(string refactorPath, string generatedFilesPath, string file);
    }

    public class JsSplitter : IJsSplitter, IHasHumanInterface
    {
        public IHumanInterface Ux { get; private set; }
        public IPageJsSeperationEvaluator PageJsSeperationEvaluator { get; private set; }
        public IDirectoryMimicker DirectoryMimicker { get; private set; }

        public JsSplitter(IHumanInterface ux, IPageJsSeperationEvaluator pageJsSeperationEvaluator, IDirectoryMimicker directoryMimicker)
        {
            DirectoryMimicker = directoryMimicker;
            PageJsSeperationEvaluator = pageJsSeperationEvaluator;
            Ux = ux;
        }

        public void Split(string refactorPath, string generatedFilesPath, string[] filesToRefactor)
        {
            var failedFiles = new List<string>();
            var collisions = new List<string>();
            var created = 0;

            for (var i = 0; i < filesToRefactor.Length; i++)
            {
                var file = filesToRefactor[i];

                //Ux.WriteLine(string.Format("Refactoring {0}", file));
                Ux.WriteLine(i.ToString());

                SeperatedJsViewModel seperatedJsViewModel;

                var newPath = DirectoryMimicker.Mimick(refactorPath, generatedFilesPath, file);

                try
                {
                    seperatedJsViewModel = PageJsSeperationEvaluator.Evaluate(File.ReadAllLines(file), Properties.Settings.Default.SolutionPath, newPath, file);
                }
                catch (Exception e)
                {
                    var x = string.Format("---ERROR: Unable to process {0}---", file);
                    //Ux.WriteLine(x);
                    //Ux.WriteLine("");
                    //MessageBox.Show(x, "OPERATION HALTED", MessageBoxButton.OK, MessageBoxImage.Stop);
                    failedFiles.Add(file);
                    continue;
                }


                if (seperatedJsViewModel.ExtractedJsBlocks.Any())
                {
                    foreach (var newFile in seperatedJsViewModel.ExtractedJsBlocks)
                    {
                        var uri = newPath + "\\" + newFile.ProposedFileName;

                        if (!Directory.Exists(newPath))
                        {
                            Directory.CreateDirectory(newPath);
                        }

                        if (File.Exists(uri))
                        {
                            //var x = string.Format("An error occured whilst attempting to process {1}{2}{2}A FILE ALREADY EXISTS HERE - PLEASE REVIEW MANUALLY - {0}", uri, file, Environment.NewLine);
                            //MessageBox.Show(x, "OVERWRITE PREVENTED", MessageBoxButton.OK, MessageBoxImage.Error);
                            //Ux.WriteLine("ERROR - TASK STOPPED: " + x);
                            //Ux.WriteLine("");
                            collisions.Add(uri);
                            Ux.WriteLine(string.Format("SKIPPED: {0}", uri));
                            continue;
                        }

                        File.WriteAllLines(uri, newFile.Lines);

                        var atSigns = newFile.Lines.Count(x => x.Contains("@"));
                        var dotDotSlashes = newFile.Lines.Count(x => x.Contains("../"));
                        if (atSigns > 0)
                        {
                            Ux.WriteLine(string.Format("---WARNING: {0} lines containing @ were detected", atSigns));
                        }
                        if (dotDotSlashes > 0)
                        {
                            Ux.WriteLine(string.Format("---WARNING: {0} lines containing ../ were detected",dotDotSlashes));
                        }

                        Ux.WriteLine("Created " + uri);
                        created++;
                    }
                    File.WriteAllLines(file, seperatedJsViewModel.StripedContent);
                }
            }

            Ux.WriteLine(string.Format("Created {0} files", created));

            if (!failedFiles.Any() && !collisions.Any())
            {
                Ux.WriteLine("Operation complete.");
                Ux.WriteLine("");
            }
            else
            {
                Ux.WriteLine(string.Format("Operation completed with {0} ERRORS.", failedFiles.Count));
                foreach (var failedFile in failedFiles)
                {
                    Ux.WriteLine("Unable to parse: " + failedFile);
                }
                foreach (var collision in collisions)
                {
                    Ux.WriteLine("Skipped overwrite: " + collision);                    
                }
                
                Ux.WriteLine("");
            }
        }
    }

}