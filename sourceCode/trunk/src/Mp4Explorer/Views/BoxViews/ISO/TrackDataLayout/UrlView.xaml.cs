//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Windows.Controls;
using CMStream.Mp4;

namespace Mp4Explorer
{
    [BoxViewType(typeof(Mp4UrlBox))]
    public partial class UrlView : UserControl, IBoxView
    {
        private Mp4UrlBox _Box;

        public UrlView()
        {
            InitializeComponent();
        }

        public Mp4Box Box
        {
            get => _Box;
            set
            {
                _Box = (Mp4UrlBox)value;
                BoxViewUtil.AddHeader(grid, "Data Entry Url Box");
                if ((_Box.Flags & 1) != 0)
                {
                    BoxViewUtil.AddField(grid, "Location:", "Same file");
                }
                else
                {
                    BoxViewUtil.AddField(grid, "Location:", _Box.Location);
                }
            }
        }
    }
}
