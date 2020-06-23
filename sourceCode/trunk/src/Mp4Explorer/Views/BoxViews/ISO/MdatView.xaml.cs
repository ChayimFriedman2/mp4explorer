//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using CMStream.Mp4;
using Mp4Explorer.Controls;

namespace Mp4Explorer
{
    [BoxViewType(typeof(Mp4MdatBox))]
    public partial class MdatView : UserControl, IBoxView
    {
        private Mp4MdatBox _Box;

        public MdatView()
        {
            InitializeComponent();
        }

        public Mp4Box Box
        {
            get => _Box;
            set
            {
                _Box = (Mp4MdatBox)value;
                byte[] data = new byte[Math.Min(32 * 1024, _Box.Size - _Box.HeaderSize)];
                _Box.Stream.Position = _Box.Position;
                _Box.Stream.Read(data, data.Length);
                var sc = new ScrollViewer
                {
                    Content = new HexView { Data = new MemoryStream(data) },
                    HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                };
                BoxViewUtil.AddHeader(grid, "Media Data Box");
                BoxViewUtil.AddSubHeader(grid, "Raw data (up to 32K bytes)");
                BoxViewUtil.AddControl(grid, sc).Height = new GridLength(400);
            }
        }
    }
}
