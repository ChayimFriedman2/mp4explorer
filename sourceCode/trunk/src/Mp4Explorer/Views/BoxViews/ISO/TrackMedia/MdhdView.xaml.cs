//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Windows.Controls;
using CMStream.Mp4;

namespace Mp4Explorer
{
    [BoxViewType(typeof(Mp4MdhdBox))]
    public partial class MdhdView : UserControl, IBoxView
    {
        private Mp4MdhdBox _Box;

        public MdhdView()
        {
            InitializeComponent();
        }

        public Mp4Box Box
        {
            get => _Box;
            set
            {
                _Box = (Mp4MdhdBox)value;
                BoxViewUtil.AddHeader(grid, "Media Header Box");
                BoxViewUtil.AddField(grid, "Creation time(UTC):", Mp4Util.FormatTime(_Box.CreationTime));
                BoxViewUtil.AddField(grid, "Modification time(UTC):", Mp4Util.FormatTime(_Box.ModificationTime));
                BoxViewUtil.AddField(grid, "Time scale:", _Box.TimeScale.ToString());
                BoxViewUtil.AddField(grid, "Duration:", _Box.Duration.ToString());
                BoxViewUtil.AddField(grid, "Language:", _Box.Language);
            }
        }
    }
}
