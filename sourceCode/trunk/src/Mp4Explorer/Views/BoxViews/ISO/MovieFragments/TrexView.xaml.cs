//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Windows.Controls;
using CMStream.Mp4;

namespace Mp4Explorer
{
    [BoxViewType(typeof(Mp4TrexBox))]
    public partial class TrexView : UserControl, IBoxView
    {
        private Mp4TrexBox _Box;

        public TrexView()
        {
            InitializeComponent();
        }

        public Mp4Box Box
        {
            get => _Box;
            set
            {
                _Box = (Mp4TrexBox)value;
                BoxViewUtil.AddHeader(grid, "Track Extends Box");
                BoxViewUtil.AddField(grid, "Track id:", _Box.TrackId.ToString());
                BoxViewUtil.AddField(grid, "Default sample description index:", _Box.DefaultSampleDescriptionIndex.ToString());
                BoxViewUtil.AddField(grid, "Default sample duration:", _Box.DefaultSampleDuration.ToString());
                BoxViewUtil.AddField(grid, "Default sample size:", _Box.DefaultSampleSize.ToString());
                BoxViewUtil.AddField(grid, "Default sample flags:", _Box.DefaultSampleFlags.ToString());
                BoxViewUtil.AddSubHeader(grid, "Default sample flags data");
                var flagsData = new Mp4SampleFlagsData(_Box.DefaultSampleFlags);
                BoxViewUtil.AddField(grid, "Sample depends on:", flagsData.SampleDependsOn.ToString());
                BoxViewUtil.AddField(grid, "Sample is depend on:", flagsData.SampleIsDependOn.ToString());
                BoxViewUtil.AddField(grid, "Sample has redudancy:", flagsData.SampleHasRedudancy.ToString());
                BoxViewUtil.AddField(grid, "Sample padding value:", flagsData.SamplePaddingValue.ToString());
                BoxViewUtil.AddField(grid, "Sample is difference sample:", flagsData.SampleIsDifferenceSample.ToString());
                BoxViewUtil.AddField(grid, "Sample degradation priority:", flagsData.SampleDegradationPriority.ToString());
            }
        }
    }
}
