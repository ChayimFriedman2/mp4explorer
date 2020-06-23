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
    [BoxViewType(typeof(Mp4ElstBox))]
    public partial class ElstView : UserControl, IBoxView
    {
        private Mp4ElstBox _Box;

        public ElstView()
        {
            InitializeComponent();
        }

        public Mp4Box Box
        {
            get => _Box;
            set
            {
                _Box = (Mp4ElstBox)value;
                BoxViewUtil.AddHeader(grid, "Edit List Box");
                BoxViewUtil.AddField(grid, "Entry count:", _Box.EntryCount.ToString());
                BoxViewUtil.AddSubHeader(grid, "Entries");
                BoxViewUtil.AddControl(grid, BuildListView(_Box)).Height = new GridLength(400, GridUnitType.Pixel);
            }
        }

        private ListView BuildListView(Mp4ElstBox elst)
        {
            var listView = new ListView();
            var grid = new GridView();
            var c1 = new GridViewColumn
            {
                DisplayMemberBinding = new Binding("SegmentDuration"),
                Header = "Segment duration",
            };
            grid.Columns.Add(c1);
            var c2 = new GridViewColumn
            {
                DisplayMemberBinding = new Binding("MediaTime"),
                Header = "Media time",
            };
            grid.Columns.Add(c2);
            var c3 = new GridViewColumn
            {
                DisplayMemberBinding = new Binding("MediaRate"),
                Header = "Media rate",
            };
            grid.Columns.Add(c3);
            listView.ItemsSource = new ObservableCollection<Mp4ElstEntry>(elst.Entries);
            listView.View = grid;
            return listView;
        }
    }
}
