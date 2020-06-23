//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Windows.Controls;
using CMStream.Mp4;

namespace Mp4Explorer
{
    [BoxViewType(typeof(Mp4HdlrBox))]
    public partial class HdlrView : UserControl, IBoxView
    {
        private Mp4HdlrBox _Box;

        public HdlrView()
        {
            InitializeComponent();
        }

        public Mp4Box Box
        {
            get => _Box;
            set
            {
                _Box = (Mp4HdlrBox)value;
                BoxViewUtil.AddHeader(grid, "Handler Reference Box");
                BoxViewUtil.AddField(grid, "Handler type:", Mp4Util.FormatFourChars(_Box.HandlerType));
                BoxViewUtil.AddField(grid, "Name:", _Box.Name);
            }
        }
    }
}
