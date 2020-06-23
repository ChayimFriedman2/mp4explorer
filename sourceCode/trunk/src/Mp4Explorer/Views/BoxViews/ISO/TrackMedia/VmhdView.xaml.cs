//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Windows.Controls;
using CMStream.Mp4;

namespace Mp4Explorer
{
    [BoxViewType(typeof(Mp4VmhdBox))]
    public partial class VmhdView : UserControl, IBoxView
    {
        private Mp4VmhdBox _Box;

        public VmhdView()
        {
            InitializeComponent();
        }

        public Mp4Box Box
        {
            get => _Box;
            set
            {
                _Box = (Mp4VmhdBox)value;
                BoxViewUtil.AddHeader(grid, "Video Media Header Box");
                BoxViewUtil.AddField(grid, "Graphics mode:", _Box.GraphicsMode);
                BoxViewUtil.AddField(grid, "Red:", _Box.OpColor[0]);
                BoxViewUtil.AddField(grid, "Green:", _Box.OpColor[1]);
                BoxViewUtil.AddField(grid, "Blue:", _Box.OpColor[2]);
            }
        }
    }
}
