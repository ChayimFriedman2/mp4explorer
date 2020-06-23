//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Windows.Controls;
using CMStream.Mp4;

namespace Mp4Explorer
{
    [BoxViewType(typeof(Mp4MfraBox))]
    public partial class MfraView : UserControl, IBoxView
    {
        private Mp4MfraBox _Box;

        public MfraView()
        {
            InitializeComponent();
        }

        public Mp4Box Box
        {
            get => _Box;
            set
            {
                _Box = (Mp4MfraBox)value;
                BoxViewUtil.AddHeader(grid, "Movie Fragment Random Access Box");
                BoxViewUtil.AddField(grid, "Size:", _Box.Size);
            }
        }
    }
}
