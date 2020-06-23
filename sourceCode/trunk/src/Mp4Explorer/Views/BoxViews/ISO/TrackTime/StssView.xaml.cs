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
    [BoxViewType(typeof(Mp4StssBox))]
    public partial class StssView : UserControl, IBoxView
    {
        private Mp4StssBox _Box;

        public StssView()
        {
            InitializeComponent();
        }

        public Mp4Box Box
        {
            get => _Box;
            set
            {
                _Box = (Mp4StssBox)value;
                BoxViewUtil.AddHeader(grid, "Sync Sample Box");
                BoxViewUtil.AddField(grid, "Entry count:", _Box.EntryCount);
                BoxViewUtil.AddSubHeader(grid, "Entries");
                BoxViewUtil.AddControl(grid, BuildListView(_Box)).Height = new GridLength(400, GridUnitType.Pixel);
            }
        }

        private ListView BuildListView(Mp4StssBox stss)
        {
            var listView = new ListView();
            var grid = new GridView();
            var c1 = new GridViewColumn
            {
                //c1.DisplayMemberBinding = new Binding("?");
                Header = "Sample number",
            };
            grid.Columns.Add(c1);
            listView.ItemsSource = new ObservableCollection<uint>(stss.Entries);
            listView.View = grid;
            return listView;
        }
    }
}
