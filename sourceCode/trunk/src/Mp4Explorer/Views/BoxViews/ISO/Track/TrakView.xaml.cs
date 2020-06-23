//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Windows.Controls;
using CMStream.Mp4;

namespace Mp4Explorer
{
    [BoxViewType(typeof(Mp4TrakBox))]
    public partial class TrakView : UserControl, IBoxView
    {
        private Mp4TrakBox _Box;

        public TrakView()
        {
            InitializeComponent();
        }

        public Mp4Box Box
        {
            get => _Box;
            set
            {
                _Box = (Mp4TrakBox)value;
                BoxViewUtil.AddHeader(grid, "Track Box");
                BoxViewUtil.AddField(grid, "Size:", _Box.Size);
            }
        }
    }
}
