//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Windows.Controls;
using CMStream.Mp4;

namespace Mp4Explorer
{
    [BoxViewType(typeof(Mp4MfhdBox))]
    public partial class MfhdView : UserControl, IBoxView
    {
        private Mp4MfhdBox _Box;

        public MfhdView()
        {
            InitializeComponent();
        }

        public Mp4Box Box
        {
            get => _Box;
            set
            {
                _Box = (Mp4MfhdBox)value;
                BoxViewUtil.AddHeader(grid, "Movie Fragment Header Box");
                BoxViewUtil.AddField(grid, "Sequence number:", _Box.SequenceNumber);
            }
        }
    }
}
