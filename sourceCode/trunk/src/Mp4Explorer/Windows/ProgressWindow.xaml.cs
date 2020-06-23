//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.ComponentModel;
using System.Windows;

namespace Mp4Explorer
{
    /// <summary>
    /// Interaction logic for ProgressWindow.xaml
    /// </summary>
    public partial class ProgressWindow : Window
    {
        private readonly BackgroundWorker _Worker;

        public ProgressWindow(BackgroundWorker worker)
        {
            InitializeComponent();

            _Worker = worker;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_Worker != null)
            {
                _Worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
                _Worker.RunWorkerAsync();
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (_Worker?.IsBusy == true)
            {
                e.Cancel = true;
            }
        }
    }
}
