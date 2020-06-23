//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Windows.Controls;
using CMStream.Mp4;

namespace Mp4Explorer
{
    [BoxViewType(typeof(Mp4MfroBox))]
    public partial class MfroView : UserControl, IBoxView
    {
        private Mp4MfroBox _Box;

        public MfroView()
        {
            InitializeComponent();
        }

        public Mp4Box Box
        {
            get => _Box;
            set
            {
                _Box = (Mp4MfroBox)value;
                BoxViewUtil.AddHeader(grid, "Movie Fragment Random Access Offset Box");
                BoxViewUtil.AddField(grid, "Mfra size:", _Box.MfraSize);
            }
        }
    }
}
