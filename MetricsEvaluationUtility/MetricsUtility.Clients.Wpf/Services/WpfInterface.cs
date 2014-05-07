using System;
using MetricsUtility.Core.Services;

namespace MetricsUtility.Clients.Wpf.Services
{
    public class WpfInterface : IHumanInterface
    {
        public void Write(string str)
        {
            WriteEvent(null, str);
        }

        public void WriteLine(string str)
        {
            WriteLineEvent(null, str);
        }

        //TODO: Possibly refactor to make agnostic?
        public string Read(string str)
        {
            ReadEvent(null, str);
            return null;
        }

        public void AddOption(string title, Action action)
        {
            AddOptionEvent(null, new AddOptionEventArgs { Action = action, Title = title });
        }

        public void DisplayOptions(string question)
        {
            DisplayOptionsEvent(null, question);
        }

        public void AddOptionWithHeadingSpace(string title, Action action)
        {
            AddOptionWithHeadingSpaceEvent(null, new AddOptionEventArgs { Action = action, Title = title });
        }

        public void DisplayBoolOption(string question, Action actionOnTrue, Action actionOnFalse)
        {
            DisplayBoolOptionEvent(null, new BoolOptionEventArgs { ActionOnFalse = actionOnFalse, ActionOnTrue = actionOnTrue, Question = question });
        }

        public event EventHandler<string> WriteEvent;
        public event EventHandler<string> WriteLineEvent;
        public event EventHandler<string> ReadEvent;
        public event EventHandler<AddOptionEventArgs> AddOptionEvent;
        public event EventHandler<string> DisplayOptionsEvent;
        public event EventHandler<AddOptionEventArgs> AddOptionWithHeadingSpaceEvent;
        public event EventHandler<BoolOptionEventArgs> DisplayBoolOptionEvent;
    }
}