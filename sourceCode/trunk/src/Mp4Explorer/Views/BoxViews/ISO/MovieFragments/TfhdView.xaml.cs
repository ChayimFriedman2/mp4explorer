//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Windows.Controls;
using CMStream.Mp4;

namespace Mp4Explorer
{
    [BoxViewType(typeof(Mp4TfhdBox))]
    public partial class TfhdView : UserControl, IBoxView
    {
        private Mp4TfhdBox _Box;

        public TfhdView()
        {
            InitializeComponent();
        }

        public Mp4Box Box
        {
            get => _Box;
            set
            {
                _Box = (Mp4TfhdBox)value;
                BoxViewUtil.AddHeader(grid, "Track Fragment Header Box");
                BoxViewUtil.AddField(grid, "Track id:", _Box.TrackId);
                if ((_Box.Flags & Mp4TfhdBox.FLAG_BASE_DATA_OFFSET_PRESENT) != 0)
                {
                    BoxViewUtil.AddField(grid, "Base data offset:", _Box.BaseDataOffset);
                }
                if ((_Box.Flags & Mp4TfhdBox.FLAG_SAMPLE_DESCRIPTION_INDEX_PRESENT) != 0)
                {
                    BoxViewUtil.AddField(grid, "Sample description index:", _Box.SampleDescriptionIndex);
                }
                if ((_Box.Flags & Mp4TfhdBox.FLAG_DEFAULT_SAMPLE_DURATION_PRESENT) != 0)
                {
                    BoxViewUtil.AddField(grid, "Default sample duration:", _Box.DefaultSampleDuration);
                }
                if ((_Box.Flags & Mp4TfhdBox.FLAG_DEFAULT_SAMPLE_SIZE_PRESENT) != 0)
                {
                    BoxViewUtil.AddField(grid, "Default sample size:", _Box.DefaultSampleSize);
                }
                if ((_Box.Flags & Mp4TfhdBox.FLAG_DEFAULT_SAMPLE_FLAGS_PRESENT) != 0)
                {
                    BoxViewUtil.AddSubHeader(grid, "Default sample flags data");
                    var flagsData = new Mp4SampleFlagsData(_Box.DefaultSampleFlags);
                    BoxViewUtil.AddField(grid, "Sample depends on:", flagsData.SampleDependsOn);
                    BoxViewUtil.AddField(grid, "Sample is depend on:", flagsData.SampleIsDependOn);
                    BoxViewUtil.AddField(grid, "Sample has redudancy:", flagsData.SampleHasRedudancy);
                    BoxViewUtil.AddField(grid, "Sample padding value:", flagsData.SamplePaddingValue);
                    BoxViewUtil.AddField(grid, "Sample is difference sample:", flagsData.SampleIsDifferenceSample);
                    BoxViewUtil.AddField(grid, "Sample degradation priority:", flagsData.SampleDegradationPriority);
                }
            }
        }
    }
}
