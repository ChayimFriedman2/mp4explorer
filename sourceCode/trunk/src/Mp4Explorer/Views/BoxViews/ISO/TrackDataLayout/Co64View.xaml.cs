//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using CMStream.Mp4;

namespace Mp4Explorer
{
    [BoxViewType(typeof(Mp4Co64Box))]
    public partial class Co64View : UserControl, IBoxView
    {
        private Mp4Co64Box _Box;

        public Co64View()
        {
            InitializeComponent();
        }

        public Mp4Box Box
        {
            get => _Box;
            set
            {
                _Box = (Mp4Co64Box)value;
                BoxViewUtil.AddHeader(grid, "64-bit Chunk Offset Box.");
                //TODO: Change
                //BoxViewUtil.AddField(grid, "Entry count:", box.EntriesCount);
                BoxViewUtil.AddField(grid, "Entry count:", _Box.Entries.Count);
                BoxViewUtil.AddSubHeader(grid, "Entries");
                BoxViewUtil.AddControl(grid, BuildListView(_Box)).Height = new GridLength(400, GridUnitType.Pixel);
            }
        }

        private ListView BuildListView(Mp4Co64Box co64)
        {
            var listView = new ListView();
            var grid = new GridView();
            var c1 = new GridViewColumn
            {
                Header = "Sample size",
            };
            grid.Columns.Add(c1);
            listView.ItemsSource = new ObservableCollection<ulong>(co64.Entries);
            listView.View = grid;
            return listView;
        }
    }
}
