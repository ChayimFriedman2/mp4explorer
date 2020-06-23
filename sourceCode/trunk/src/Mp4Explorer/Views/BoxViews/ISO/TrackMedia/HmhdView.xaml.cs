//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Windows.Controls;
using CMStream.Mp4;

namespace Mp4Explorer
{
    [BoxViewType(typeof(Mp4HmhdBox))]
    public partial class HmhdView : UserControl, IBoxView
    {
        private Mp4HmhdBox _Box;

        public HmhdView()
        {
            InitializeComponent();
        }

        public Mp4Box Box
        {
            get => _Box;
            set
            {
                _Box = (Mp4HmhdBox)value;
                BoxViewUtil.AddHeader(grid, "Hint Media Header Box");
                BoxViewUtil.AddField(grid, "Maximum PDU size:", _Box.MaxPduSize);
                BoxViewUtil.AddField(grid, "Average PDU size:", _Box.AvgPduSize);
                BoxViewUtil.AddField(grid, "Maximum bitrate:", _Box.MaxBitrate);
                BoxViewUtil.AddField(grid, "Average bitrate:", _Box.AvgBitrate);
            }
        }
    }
}
