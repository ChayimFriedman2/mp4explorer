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
    [BoxViewType(typeof(Mp4StszBox))]
    public partial class StszView : UserControl, IBoxView
    {
        private Mp4StszBox _Box;

        public StszView()
        {
            InitializeComponent();
        }

        public Mp4Box Box
        {
            get => _Box;
            set
            {
                _Box = (Mp4StszBox)value;
                BoxViewUtil.AddHeader(grid, "Sample Size Box.");
                BoxViewUtil.AddField(grid, "Sample size:", _Box.SampleSize);
                BoxViewUtil.AddField(grid, "Sample count:", _Box.SampleCount);
                BoxViewUtil.AddSubHeader(grid, "Entries");
                BoxViewUtil.AddControl(grid, BuildListView(_Box)).Height = new GridLength(380, GridUnitType.Pixel);
            }
        }

        private ListView BuildListView(Mp4StszBox stsz)
        {
            var listView = new ListView();
            var grid = new GridView();
            var c1 = new GridViewColumn
            {
                //c1.DisplayMemberBinding = new Binding("?");
                Header = "Sample size",
            };
            grid.Columns.Add(c1);
            listView.ItemsSource = new ObservableCollection<uint>(stsz.Entries);
            listView.View = grid;
            return listView;
        }
    }
}
