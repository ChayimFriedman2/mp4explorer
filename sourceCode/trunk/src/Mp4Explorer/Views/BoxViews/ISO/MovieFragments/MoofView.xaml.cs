//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Windows.Controls;
using CMStream.Mp4;

namespace Mp4Explorer
{
    [BoxViewType(typeof(Mp4MoofBox))]
    public partial class MoofView : UserControl, IBoxView
    {
        private Mp4MoofBox _Box;

        public MoofView()
        {
            InitializeComponent();
        }

        public Mp4Box Box
        {
            get => _Box;
            set
            {
                _Box = (Mp4MoofBox)value;
                BoxViewUtil.AddHeader(grid, "Movie Fragment Box");
                BoxViewUtil.AddField(grid, "Size:", _Box.Size);
            }
        }
    }
}
