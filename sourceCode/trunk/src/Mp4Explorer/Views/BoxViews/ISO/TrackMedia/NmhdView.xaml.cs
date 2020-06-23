//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Windows.Controls;
using CMStream.Mp4;

namespace Mp4Explorer
{
    [BoxViewType(typeof(Mp4NmhdBox))]
    public partial class NmhdView : UserControl, IBoxView
    {
        private Mp4NmhdBox _Box;

        public NmhdView()
        {
            InitializeComponent();
        }

        public Mp4Box Box
        {
            get => _Box;
            set
            {
                _Box = (Mp4NmhdBox)value;
                BoxViewUtil.AddHeader(grid, "Null Media Header Box");
            }
        }
    }
}
