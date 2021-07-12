using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

namespace RevitAddin_ProgressBar.Views
{
    public partial class ProgressBar2View : Window, IDisposable
    {
        private long milliseconds = 20;
        private Stopwatch stopwatch { get; set; }
        public bool IsClosed { get; private set; }
        public ProgressBar2View(string title = "")
        {
            InitializeComponent();
            InitializeSize();
            InitializaStopwatch();
            this.Title = title;
            this.Closed += (s, e) =>
            {
                IsClosed = true;
            };
        }

        private void InitializaStopwatch()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
        }

        public bool Run(string title, int count, Action<int> action)
        {
            this.Title = title;
            return Run(count, action);
        }

        public bool Run(int count, Action<int> action)
        {
            if (IsClosed) return IsClosed;
            Show();
            this.progressBar.Value = 0;
            this.progressBar.Maximum = count;
            for (int i = 0; i < count; i++)
            {
                action?.Invoke(i);
                if (Update()) break;
            }
            return IsClosed;
        }

        public bool Run<T>(string title, IEnumerable<T> collection, Action<T> action)
        {
            this.Title = title;
            return Run(collection, action);
        }

        public bool Run<T>(IEnumerable<T> collection, Action<T> action)
        {
            if (IsClosed) return IsClosed;
            Show();
            this.progressBar.Value = 0;
            this.progressBar.Maximum = collection.Count();
            foreach (var item in collection)
            {
                action?.Invoke(item);
                if (Update()) break;
            }
            return IsClosed;
        }

        private bool Update(double value = 1.0)
        {
            if (stopwatch.ElapsedMilliseconds > milliseconds)
            {
                DoEvents();
                stopwatch.Restart();
            }
            progressBar.Value += value;
            return IsClosed;
        }

        private void DoEvents()
        {
            System.Windows.Forms.Application.DoEvents();
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
        }

        private void InitializeSize()
        {
            this.SizeToContent = SizeToContent.WidthAndHeight;
            this.Topmost = true;
            this.ShowInTaskbar = false;
            this.ResizeMode = ResizeMode.NoResize;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        public void Dispose()
        {
            if (!IsClosed) Close();
        }
    }
}