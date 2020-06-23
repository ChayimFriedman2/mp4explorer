//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System;
using System.Windows;

namespace Mp4Explorer
{
    public partial class ErrorWindow : Window
    {
        public ErrorWindow(Exception e)
        {
            InitializeComponent();

            if (e != null)
            {
                ErrorTextBox.Text = $@"{e.Message}

{e.StackTrace}";
            }
        }

        public ErrorWindow(string message, Exception e)
        {
            InitializeComponent();

            if (e != null)
            {
                ErrorTextBox.Text = $@"{message}
{e.Message}

{e.StackTrace}";
            }
        }

        public ErrorWindow(Uri uri)
        {
            InitializeComponent();

            if (uri != null)
            {
                ErrorTextBox.Text = $"Page not found: \"{uri}\"";
            }
        }

        public ErrorWindow(string message) : this(message, "")
        {
        }

        public ErrorWindow(string message, string details)
        {
            InitializeComponent();

            ErrorTextBox.Text = $@"{message}

{details}";
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
