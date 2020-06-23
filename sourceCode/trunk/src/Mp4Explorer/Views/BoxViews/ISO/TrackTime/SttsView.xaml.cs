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
    [BoxViewType(typeof(Mp4SttsBox))]
    public partial class SttsView : UserControl, IBoxView
    {
        private Mp4SttsBox _Box;

        public SttsView()
        {
            InitializeComponent();
        }

        public Mp4Box Box
        {
            get => _Box;
            set
            {
                _Box = (Mp4SttsBox)value;
                BoxViewUtil.AddHeader(grid, "Decoding Time to Sample Box");
                BoxViewUtil.AddField(grid, "Entry count:", _Box.EntryCount);
                BoxViewUtil.AddSubHeader(grid, "Entries");
                BoxViewUtil.AddControl(grid, BuildListView(_Box)).Height = new GridLength(400, GridUnitType.Pixel);
            }
        }

        private ListView BuildListView(Mp4SttsBox stts)
        {
            var listView = new ListView();
            var grid = new GridView();
            var c1 = new GridViewColumn
            {
                DisplayMemberBinding = new Binding("SampleCount"),
                Header = "Sample count",
            };
            grid.Columns.Add(c1);
            var c2 = new GridViewColumn
            {
                DisplayMemberBinding = new Binding("SampleDelta"),
                Header = "Sample delta",
            };
            grid.Columns.Add(c2);
            listView.ItemsSource = new ObservableCollection<Mp4SttsEntry>(stts.Entries);
            listView.View = grid;
            return listView;
        }
    }
}
