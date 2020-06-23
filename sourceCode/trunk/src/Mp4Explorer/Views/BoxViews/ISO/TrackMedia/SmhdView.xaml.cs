//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Windows.Controls;
using CMStream.Mp4;

namespace Mp4Explorer
{
    [BoxViewType(typeof(Mp4SmhdBox))]
    public partial class SmhdView : UserControl, IBoxView
    {
        private Mp4SmhdBox _Box;

        public SmhdView()
        {
            InitializeComponent();
        }

        public Mp4Box Box
        {
            get => _Box;
            set
            {
                _Box = (Mp4SmhdBox)value;
                BoxViewUtil.AddHeader(grid, "Sound Media Header Box");
                BoxViewUtil.AddField(grid, "Balance:", Mp4Util.FormatFloat((ushort)_Box.Balance));
            }
        }
    }
}
