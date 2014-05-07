using System;
using System.Collections.Generic;
using MetricsUtility.Core.Services;

namespace MetricsUtility.Clients.ConsoleApp.Services
{
    public class ConsoleAppHumanInterface : IHumanInterface
    {
        private List<Option> _options;

        public ConsoleAppHumanInterface()
        {
            _options = new List<Option>();
        }

        public void Write(string str)
        {
            Console.Write(str);
        }

        public void WriteLine(string str)
        {
            Console.WriteLine(str);
        }

        public string Read(string str)
        {
            Console.Write("{0}: ", str);
            return Console.ReadLine();
        }

        public void AddOption(string title, Action action)
        {
            _options.Add(new Option
            {
                Action = action,
                Title = title,
                Index = _options.Count
            });
        }

        public void DisplayOptions(string question)
        {
            Console.WriteLine("{0}{0}---{0}Menu{0}---{0}{0}", Environment.NewLine);

            WriteLine(string.Format("{0}:{1}", question,Environment.NewLine));

            foreach (var option in _options)
            {
                if (option.HasSpacingLine)
                {
                    Console.WriteLine("");
                }
                Console.WriteLine("[{0}] {1}", option.Index, option.Title);
            }

            int selectedOption = -1;
            while (selectedOption == -1)
            {
                Console.Write("{0}Selected: ", Environment.NewLine);
                var val = Console.ReadLine();
                if (!int.TryParse(val, out selectedOption))
                {
                    selectedOption = -1;
                    Console.WriteLine("Invalid option, please try again.");
                }
                else if (selectedOption < 0 || selectedOption >= _options.Count)
                {
                    selectedOption = -1;
                    Console.WriteLine("Invalid option, please try again.");
                }
            }

            Console.WriteLine("");
            if (_options[selectedOption].Action != null)
            {
                _options[selectedOption].Action();
            }
            ClearOptions();
        }

        public void AddOptionWithHeadingSpace(string title, Action action)
        {
            _options.Add(new Option
            {
                Action = action,
                Title = title,
                Index = _options.Count,
                HasSpacingLine = true
            });
        }

        public void DisplayBoolOption(string question, Action actionOnTrue, Action actionOnFalse)
        {
            WriteLine("");
            AddOption("No", actionOnFalse);
            AddOption("Yes", actionOnTrue);
            DisplayOptions(question);
        }

        public void UpdateProgress(int value)
        {
            WriteLine(string.Format("{0}%", value));
        }

        public void ResetProgress()
        {
            //Nothing
        }

        public event EventHandler<string> WriteEvent;
        public event EventHandler<string> WriteLineEvent;
        public event EventHandler<int> ProgressEvent;
        public event EventHandler<string> ReadEvent;
        public event EventHandler<AddOptionEventArgs> AddOptionEvent;
        public event EventHandler<string> DisplayOptionsEvent;
        public event EventHandler<AddOptionEventArgs> AddOptionWithHeadingSpaceEvent;
        public event EventHandler<BoolOptionEventArgs> DisplayBoolOptionEvent;
        public event EventHandler ResetProgressEvent;

        private void ClearOptions()
        {
            _options = new List<Option>();
        }
    }

    internal class Option
    {
        public string Title { get; set; }
        public Action Action { get; set; }
        public int Index { get; set; }
        public bool HasSpacingLine { get; set; }
    }
}