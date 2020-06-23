//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Windows;

namespace Mp4Explorer
{
    public partial class Shell : Window
    {
        public Shell()
        {
            InitializeComponent();
        }

        private void MenuItemAbout_Click(object sender, RoutedEventArgs e)
        {
            var aboutBoxWindow = new AboutBoxWindow
            {
                Owner = this
            };
            aboutBoxWindow.ShowDialog();
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuItemCreateFixedOffset_Click(object sender, RoutedEventArgs e)
        {
            var createFoxedOffsetWindow = new CreateFixedOffsetWindow
            {
                Owner = this
            };
            createFoxedOffsetWindow.ShowDialog();
        }

        private void MenuItemUploadToAzure_Click(object sender, RoutedEventArgs e)
        {
            var uploadToAzureWindow = new UploadToAzureWindow
            {
                Owner = this
            };
            uploadToAzureWindow.ShowDialog();
        }
    }
}
