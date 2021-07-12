using System;
using System.Windows;

namespace RevitAddin_ProgressBar.Views
{
    public partial class ProgressBarView : Window, IDisposable
    {
        public bool IsClosed { get; private set; }

        public ProgressBarView(string title = "", double maximum = 100.0)
        {
            InitializeComponent();
            InitializeSize();
            this.Title = title;
            this.progressBar.Maximum = maximum;
            this.Closed += (s, e) =>
            {
                IsClosed = true;
            };
        }

        private void InitializeSize()
        {
            this.SizeToContent = SizeToContent.WidthAndHeight;
            this.Topmost = true;
            this.ShowInTaskbar = false;
            this.ResizeMode = ResizeMode.NoResize;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        public bool Update(double value = 1.0)
        {
            DoEvents();
            progressBar.Value += value;
            return IsClosed;
        }

        private void DoEvents()
        {
            System.Windows.Forms.Application.DoEvents();
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
        }

        public void Dispose()
        {
            if (!IsClosed) Close();
        }
    }
}