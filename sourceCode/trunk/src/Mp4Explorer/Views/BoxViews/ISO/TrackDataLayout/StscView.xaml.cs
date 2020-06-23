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
    [BoxViewType(typeof(Mp4StscBox))]
    public partial class StscView : UserControl, IBoxView
    {
        private Mp4StscBox _Box;

        public StscView()
        {
            InitializeComponent();
        }

        public Mp4Box Box
        {
            get => _Box;
            set
            {
                _Box = (Mp4StscBox)value;
                BoxViewUtil.AddHeader(grid, "Sample To Chunk Box");
                BoxViewUtil.AddField(grid, "Entry count:", _Box.EntryCount);
                BoxViewUtil.AddSubHeader(grid, "Entries");
                BoxViewUtil.AddControl(grid, BuildListView(_Box)).Height = new GridLength(400, GridUnitType.Pixel);
            }
        }

        private ListView BuildListView(Mp4StscBox trun)
        {
            var listView = new ListView();
            var grid = new GridView();
            var c1 = new GridViewColumn
            {
                DisplayMemberBinding = new Binding("FirstChunk"),
                Header = "First chunk",
            };
            grid.Columns.Add(c1);
            var c2 = new GridViewColumn
            {
                DisplayMemberBinding = new Binding("SamplesPerChunk"),
                Header = "Samples per chunk",
            };
            grid.Columns.Add(c2);
            var c3 = new GridViewColumn
            {
                DisplayMemberBinding = new Binding("SampleDescriptionIndex"),
                Header = "Sample description index",
            };
            grid.Columns.Add(c3);
            listView.ItemsSource = new ObservableCollection<Mp4StscEntry>(trun.Entries);
            listView.View = grid;
            return listView;
        }
    }
}
