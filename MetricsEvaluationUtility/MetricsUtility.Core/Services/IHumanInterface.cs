using System;

namespace MetricsUtility.Core.Services
{
    public interface IHumanInterface
    {
        void Write(string str);
        void WriteLine(string str);
        string Read(string str);
        void AddOption(string title, Action action);
        void DisplayOptions(string question);
        void AddOptionWithHeadingSpace(string title, Action action);
        void DisplayBoolOption(string question, Action actionOnTrue, Action actionOnFalse);
        void UpdateProgress(int value);
        void ResetProgress();

        event EventHandler<string> WriteEvent;
        event EventHandler<string> WriteLineEvent;
        event EventHandler<int> ProgressEvent;
        event EventHandler<string> ReadEvent;
        event EventHandler<AddOptionEventArgs> AddOptionEvent;
        event EventHandler<string> DisplayOptionsEvent;
        event EventHandler<AddOptionEventArgs> AddOptionWithHeadingSpaceEvent;
        event EventHandler<BoolOptionEventArgs> DisplayBoolOptionEvent;
        event EventHandler ResetProgressEvent;
    }

    public class BoolOptionEventArgs
    {
        public string Question { get; set; }
        public Action ActionOnTrue { get; set; }
        public Action ActionOnFalse { get; set; }
    }

    public class AddOptionEventArgs
    {
        public Action Action { get; set; }
        public string Title { get; set; }
    }
}