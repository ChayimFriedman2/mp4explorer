//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Windows.Controls;
using CMStream.Mp4;

namespace Mp4Explorer
{
    [BoxViewType(typeof(Mp4IodsBox))]
    public partial class IodsView : UserControl, IBoxView
    {
        private Mp4IodsBox _Box;

        public IodsView()
        {
            InitializeComponent();
        }

        public Mp4Box Box
        {
            get => _Box;
            set
            {
                _Box = (Mp4IodsBox)value;
                BoxViewUtil.AddHeader(grid, "Object Descriptor Box");
            }
        }
    }
}
