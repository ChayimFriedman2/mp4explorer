//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Windows.Controls;
using CMStream.Mp4;

namespace Mp4Explorer
{
    [BoxViewType(typeof(Mp4AvccBox))]
    public partial class AvccView : UserControl, IBoxView
    {
        private Mp4AvccBox _Box;

        public AvccView()
        {
            InitializeComponent();
        }

        public Mp4Box Box
        {
            get => _Box;
            set
            {
                _Box = (Mp4AvccBox)value;
                BoxViewUtil.AddHeader(grid, "AVC Decoder Configuration Record");
                BoxViewUtil.AddField(grid, "Configuration version:", _Box.ConfigurationVersion);
                BoxViewUtil.AddField(grid, "AVC profile indication:", Mp4Util.GetProfileName(_Box.AVCProfileIndication));
                BoxViewUtil.AddField(grid, "AVC level indication:", _Box.AVCLevelIndication);
                BoxViewUtil.AddField(grid, "AVC compatible profiles:", _Box.AVCCompatibleProfiles.ToString("X"));
                BoxViewUtil.AddField(grid, "NALU length size:", _Box.NaluLengthSize);
                for (int i = 0; i < _Box.SequenceParameters.Count; i++)
                {
                    BoxViewUtil.AddField(grid, "Sequence parameter " + i + ":", _Box.SequenceParameters[i]);
                }
                for (int i = 0; i < _Box.PictureParameters.Count; i++)
                {
                    BoxViewUtil.AddField(grid, "Picture parameter " + i + ":", _Box.PictureParameters[i]);
                }
            }
        }
    }
}
