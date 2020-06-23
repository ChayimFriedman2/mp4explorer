//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Windows.Controls;
using CMStream.Mp4;

namespace Mp4Explorer
{
    [BoxViewType(typeof(Mp4MvhdBox))]
    public partial class MvhdView : UserControl, IBoxView
    {
        private Mp4MvhdBox _Box;

        public MvhdView()
        {
            InitializeComponent();
        }

        public Mp4Box Box
        {
            get => _Box;
            set
            {
                _Box = (Mp4MvhdBox)value;
                BoxViewUtil.AddHeader(grid, "Movie Header Box");
                BoxViewUtil.AddField(grid, "Creation time(UTC):", Mp4Util.FormatTime(_Box.CreationTime));
                BoxViewUtil.AddField(grid, "Modification time(UTC):", Mp4Util.FormatTime(_Box.ModificationTime));
                BoxViewUtil.AddField(grid, "Time scale:", _Box.TimeScale.ToString());
                BoxViewUtil.AddField(grid, "Duration:", _Box.Duration.ToString());
                BoxViewUtil.AddField(grid, "Duration(ms):", Mp4Util.ConvertTime(_Box.Duration, _Box.TimeScale, 1000));
                BoxViewUtil.AddField(grid, "Rate:", Mp4Util.FormatDouble(_Box.Rate));
                BoxViewUtil.AddField(grid, "Volume:", Mp4Util.FormatFloat(_Box.Volume));
                BoxViewUtil.AddField(grid, "Next track id:", _Box.NextTrackId.ToString());
            }
        }
    }
}
