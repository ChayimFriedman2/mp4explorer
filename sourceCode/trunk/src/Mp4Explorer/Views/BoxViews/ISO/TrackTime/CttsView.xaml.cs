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
    [BoxViewType(typeof(Mp4CttsBox))]
    public partial class CttsView : UserControl, IBoxView
    {
        private Mp4CttsBox _Box;

        public CttsView()
        {
            InitializeComponent();
        }

        public Mp4Box Box
        {
            get => _Box;
            set
            {
                _Box = (Mp4CttsBox)value;
                BoxViewUtil.AddHeader(grid, "Composition Time to Sample Box");
                //TODO: Change
                //BoxViewUtil.AddField(grid, "Entry count:", box.EntryCount);
                BoxViewUtil.AddField(grid, "Entry count:", _Box.Entries.Count);
                BoxViewUtil.AddSubHeader(grid, "Entries");
                BoxViewUtil.AddControl(grid, BuildListView(_Box)).Height = new GridLength(400, GridUnitType.Pixel);
            }
        }

        private ListView BuildListView(Mp4CttsBox ctts)
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
                DisplayMemberBinding = new Binding("SampleOffset"),
                Header = "Sample offset",
            };
            grid.Columns.Add(c2);
            listView.ItemsSource = new ObservableCollection<Mp4CttsEntry>(ctts.Entries);
            listView.View = grid;
            return listView;
        }
    }
}
