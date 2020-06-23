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
    [BoxViewType(typeof(Mp4StcoBox))]
    public partial class StcoView : UserControl, IBoxView
    {
        private Mp4StcoBox _Box;

        public StcoView()
        {
            InitializeComponent();
        }

        public Mp4Box Box
        {
            get => _Box;
            set
            {
                _Box = (Mp4StcoBox)value;
                BoxViewUtil.AddHeader(grid, "Chunk Offset Box.");
                //TODO: Change
                //BoxViewUtil.AddField(grid, "Entry count:", box.EntriesCount);
                BoxViewUtil.AddField(grid, "Entry count:", _Box.Entries.Count);
                BoxViewUtil.AddSubHeader(grid, "Entries");
                BoxViewUtil.AddControl(grid, BuildListView(_Box)).Height = new GridLength(400, GridUnitType.Pixel);
            }
        }

        private ListView BuildListView(Mp4StcoBox stco)
        {
            var listView = new ListView();
            var grid = new GridView();
            var c1 = new GridViewColumn
            {
                Header = "Sample size",
            };
            grid.Columns.Add(c1);
            listView.ItemsSource = new ObservableCollection<uint>(stco.Entries);
            listView.View = grid;
            return listView;
        }
    }
}
