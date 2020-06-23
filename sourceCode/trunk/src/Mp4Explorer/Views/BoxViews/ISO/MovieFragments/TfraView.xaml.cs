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
    [BoxViewType(typeof(Mp4TfraBox))]
    public partial class TfraView : UserControl, IBoxView
    {
        private Mp4TfraBox _Box;

        public TfraView()
        {
            InitializeComponent();
        }

        public Mp4Box Box
        {
            get => _Box;
            set
            {
                _Box = (Mp4TfraBox)value;
                BoxViewUtil.AddHeader(grid, "Track Fragment Random Access Box");
                BoxViewUtil.AddField(grid, "Track id:", _Box.TrackId);
                BoxViewUtil.AddField(grid, "Length size of traf num:", _Box.LengthSizeOfTrafNum);
                BoxViewUtil.AddField(grid, "Length size of trun num:", _Box.LengthSizeOfTrunNum);
                BoxViewUtil.AddField(grid, "Length size of sample num:", _Box.LengthSizeOfSampleNum);
                BoxViewUtil.AddField(grid, "Number of entries:", _Box.NumberOfEntry);
                BoxViewUtil.AddSubHeader(grid, "Entries");
                BoxViewUtil.AddControl(grid, BuildListView(_Box)).Height = new GridLength(200, GridUnitType.Pixel);
            }
        }

        private ListView BuildListView(Mp4TfraBox tfra)
        {
            var listView = new ListView();
            var grid = new GridView();
            var c1 = new GridViewColumn
            {
                DisplayMemberBinding = new Binding("Time"),
                Header = "Time",
            };
            grid.Columns.Add(c1);
            var c2 = new GridViewColumn
            {
                DisplayMemberBinding = new Binding("MoofOffset"),
                Header = "Moof offset",
            };
            grid.Columns.Add(c2);
            var c3 = new GridViewColumn
            {
                DisplayMemberBinding = new Binding("TrafNumber"),
                Header = "Traf number",
            };
            grid.Columns.Add(c3);
            var c4 = new GridViewColumn
            {
                DisplayMemberBinding = new Binding("TrunNumber"),
                Header = "Trun number",
            };
            grid.Columns.Add(c4);
            var c5 = new GridViewColumn
            {
                DisplayMemberBinding = new Binding("SampleNumber"),
                Header = "Sample number,"
            };
            grid.Columns.Add(c5);
            listView.ItemsSource = new ObservableCollection<Mp4TfraEntry>(tfra.Entries);
            listView.View = grid;
            return listView;
        }
    }
}
