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
    [BoxViewType(typeof(Mp4PdinBox))]
    public partial class PdinView : UserControl, IBoxView
    {
        private Mp4PdinBox _Box;

        public PdinView()
        {
            InitializeComponent();
        }

        public Mp4Box Box
        {
            get => _Box;
            set
            {
                _Box = (Mp4PdinBox)value;
                BoxViewUtil.AddHeader(grid, "Progressive Download Information Box");
                BoxViewUtil.AddField(grid, "Entry count:", _Box.Entries.Count);
                BoxViewUtil.AddSubHeader(grid, "Entries");
                BoxViewUtil.AddControl(grid, BuildListView(_Box)).Height = new GridLength(400, GridUnitType.Pixel);
            }
        }

        private ListView BuildListView(Mp4PdinBox pdin)
        {
            var listView = new ListView();
            var grid = new GridView();
            var c1 = new GridViewColumn
            {
                DisplayMemberBinding = new Binding("InitialDelay"),
                Header = "Initial delay",
            };
            grid.Columns.Add(c1);
            var c2 = new GridViewColumn
            {
                DisplayMemberBinding = new Binding("Rate"),
                Header = "Rate",
            };
            grid.Columns.Add(c2);
            listView.ItemsSource = new ObservableCollection<Mp4PdinEntry>(pdin.Entries);
            listView.View = grid;
            return listView;
        }
    }
}
