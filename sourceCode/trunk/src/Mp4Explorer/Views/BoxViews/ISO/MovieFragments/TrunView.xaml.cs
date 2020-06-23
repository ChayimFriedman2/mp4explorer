//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using CMStream.Mp4;

namespace Mp4Explorer
{
    [BoxViewType(typeof(Mp4TrunBox))]
    public partial class TrunView : UserControl, IBoxView
    {
        private Mp4TrunBox _Box;

        public TrunView()
        {
            InitializeComponent();
        }

        public Mp4Box Box
        {
            get => _Box;
            set
            {
                _Box = (Mp4TrunBox)value;
                BoxViewUtil.AddHeader(grid, "Track Fragment Run Box");
                BoxViewUtil.AddField(grid, "Sample count:", _Box.Entries.Count);
                BoxViewUtil.AddField(grid, "Data offset:", _Box.DataOffset);
                BoxViewUtil.AddSubHeader(grid, "First sample flags data");
                var flagsData = new Mp4SampleFlagsData(_Box.FirstSampleFlags);
                BoxViewUtil.AddField(grid, "Sample depends on:", flagsData.SampleDependsOn);
                BoxViewUtil.AddField(grid, "Sample is depend on:", flagsData.SampleIsDependOn);
                BoxViewUtil.AddField(grid, "Sample has redudancy:", flagsData.SampleHasRedudancy);
                BoxViewUtil.AddField(grid, "Sample padding value:", flagsData.SamplePaddingValue);
                BoxViewUtil.AddField(grid, "Sample is difference sample:", flagsData.SampleIsDifferenceSample);
                BoxViewUtil.AddField(grid, "Sample degradation priority:", flagsData.SampleDegradationPriority);
                BoxViewUtil.AddSubHeader(grid, "Samples");
                BoxViewUtil.AddControl(grid, BuildListView(_Box)).Height = new GridLength(200, GridUnitType.Pixel);
            }
        }

        private ListView BuildListView(Mp4TrunBox trun)
        {
            var listView = new ListView();
            var grid = new GridView();
            var c1 = new GridViewColumn
            {
                DisplayMemberBinding = new Binding("SampleDuration"),
                Header = "Duration",
            };
            grid.Columns.Add(c1);
            var c2 = new GridViewColumn
            {
                DisplayMemberBinding = new Binding("SampleSize"),
                Header = "Size",
            };
            grid.Columns.Add(c2);
            var c3 = new GridViewColumn
            {
                DisplayMemberBinding = new Binding("SampleFlags"),
                Header = "Flags",
            };
            grid.Columns.Add(c3);
            var c4 = new GridViewColumn
            {
                DisplayMemberBinding = new Binding("SampleCompositionTimeOffset"),
                Header = "Composition time offset",
            };
            grid.Columns.Add(c4);
            listView.ItemsSource = new ObservableCollection<Mp4TrunEntry>(trun.Entries);
            listView.View = grid;
            return listView;
        }
    }
}
