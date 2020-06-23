//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Windows.Controls;
using CMStream.Mp4;

namespace Mp4Explorer
{
    [BoxViewType(typeof(Mp4FtypBox))]
    public partial class FtypView : UserControl, IBoxView
    {
        private Mp4FtypBox _Box;

        public FtypView()
        {
            InitializeComponent();
        }

        public Mp4Box Box
        {
            get => _Box;
            set
            {
                _Box = (Mp4FtypBox)value;
                BoxViewUtil.AddHeader(grid, "File Type Box");
                BoxViewUtil.AddField(grid, "Major brand:", Mp4Util.FormatFourChars(_Box.MajorBrand));
                BoxViewUtil.AddField(grid, "Minor version:", _Box.MinorVersion.ToString());
                BoxViewUtil.AddField(grid, "Compatible brands:", Mp4Util.FormatFourChars(_Box.CompatibleBrands));
            }
        }
    }
}
