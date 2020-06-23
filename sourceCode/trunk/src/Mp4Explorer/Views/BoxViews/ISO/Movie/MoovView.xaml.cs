//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Windows.Controls;
using CMStream.Mp4;

namespace Mp4Explorer
{
    [BoxViewType(typeof(Mp4MoovBox))]
    public partial class MoovView : UserControl, IBoxView
    {
        private Mp4MoovBox _Box;

        public MoovView()
        {
            InitializeComponent();
        }

        public Mp4Box Box
        {
            get => _Box;
            set
            {
                _Box = (Mp4MoovBox)value;
                BoxViewUtil.AddHeader(grid, "Movie Box");
                BoxViewUtil.AddField(grid, "Size:", _Box.Size);
            }
        }
    }
}
