//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Mp4Explorer
{
    public class ImageTreeViewItem : TreeViewItem
    {
        private readonly TextBlock _TextBlock;
        private readonly Image _Image;

        public ImageTreeViewItem()
        {
            var header = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };
            Header = header;
            _Image = new Image
            {
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 2, 0)
            };
            header.Children.Add(_Image);
            _TextBlock = new TextBlock
            {
                Margin = new Thickness(0, 0, 4, 0),
                VerticalAlignment = VerticalAlignment.Center
            };
            header.Children.Add(_TextBlock);
        }
        public virtual string Text
        {
            get => _TextBlock.Text;
            set => _TextBlock.Text = value;
        }
        public virtual string ImageSource
        {
            get
            {
                try
                {
                    return _Image.Source.ToString();
                }
                catch
                {
                    return null;
                }
            }
            set
            {
                if (value != null)
                {
                    var bmp = new BitmapImage();
                    bmp.BeginInit();
                    bmp.UriSource = new Uri(value, UriKind.Relative);
                    bmp.EndInit();
                    _Image.Stretch = Stretch.Fill;
                    _Image.Source = bmp;
                }
            }
        }
    }
}
