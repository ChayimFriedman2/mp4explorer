//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Windows.Controls;
using CMStream.Mp4;

namespace Mp4Explorer
{
    [BoxViewType(typeof(Mp4TkhdBox))]
    public partial class TkhdView : UserControl, IBoxView
    {
        private Mp4TkhdBox _Box;

        public TkhdView()
        {
            InitializeComponent();
        }

        public Mp4Box Box
        {
            get => _Box;
            set
            {
                _Box = (Mp4TkhdBox)value;
                BoxViewUtil.AddHeader(grid, "Track Header Box");
                BoxViewUtil.AddField(grid, "Flags:", FormatFlags(_Box.Flags.ToString()));
                BoxViewUtil.AddField(grid, "Creation time(UTC):", Mp4Util.FormatTime(_Box.CreationTime));
                BoxViewUtil.AddField(grid, "Modification time(UTC):", Mp4Util.FormatTime(_Box.ModificationTime));
                BoxViewUtil.AddField(grid, "Track id:", _Box.TrackId.ToString());
                BoxViewUtil.AddField(grid, "Duration:", _Box.Duration.ToString());
                BoxViewUtil.AddField(grid, "Layer:", _Box.Layer.ToString());
                BoxViewUtil.AddField(grid, "Alternate group:", _Box.AlternateGroup.ToString());
                BoxViewUtil.AddField(grid, "Volume:", Mp4Util.FormatFloat(_Box.Volume));
                BoxViewUtil.AddField(grid, "Width:", Mp4Util.FormatDouble(_Box.Width));
                BoxViewUtil.AddField(grid, "Height:", Mp4Util.FormatDouble(_Box.Height));
            }
        }

        private string FormatFlags(string flagsValue)
        {
            if ((_Box.Flags & Mp4Track.TRACK_FLAG_ENABLED) != 0)
            {
                flagsValue += " ENABLED";
            }
            if ((_Box.Flags & Mp4Track.TRACK_FLAG_IN_MOVIE) != 0)
            {
                flagsValue += " IN-MOVIE";
            }
            if ((_Box.Flags & Mp4Track.TRACK_FLAG_IN_PREVIEW) != 0)
            {
                flagsValue += " IN-PREVIEW";
            }
            return flagsValue;
        }
    }
}
