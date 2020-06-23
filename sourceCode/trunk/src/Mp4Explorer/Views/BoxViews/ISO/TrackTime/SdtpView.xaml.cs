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
    [BoxViewType(typeof(Mp4SdtpBox))]
    public partial class SdtpView : UserControl, IBoxView
    {
        private Mp4SdtpBox _Box;

        public SdtpView()
        {
            InitializeComponent();
        }

        public Mp4Box Box
        {
            get => _Box;
            set
            {
                _Box = (Mp4SdtpBox)value;
                BoxViewUtil.AddHeader(grid, "Independent and Disposable Samples Box");
                //TODO: Change
                BoxViewUtil.AddField(grid, "Entry count:", _Box.Entries.Count);
                BoxViewUtil.AddSubHeader(grid, "Entries");
                BoxViewUtil.AddControl(grid, BuildListView(_Box)).Height = new GridLength(400, GridUnitType.Pixel);
            }
        }
        private ListView BuildListView(Mp4SdtpBox sdtp)
        {
            var listView = new ListView();
            var grid = new GridView();
            var c1 = new GridViewColumn
            {
                DisplayMemberBinding = new Binding("SampleDependsOn"),
                Header = "Sample depends on",
            };
            grid.Columns.Add(c1);
            var c2 = new GridViewColumn
            {
                DisplayMemberBinding = new Binding("SampleIsDependOn"),
                Header = "Sample is depend on",
            };
            grid.Columns.Add(c2);
            var c3 = new GridViewColumn
            {
                DisplayMemberBinding = new Binding("SampleHasRedundancy"),
                Header = "Sample has redundancy",
            };
            grid.Columns.Add(c3);
            listView.ItemsSource = new ObservableCollection<Mp4SdtpEntry>(sdtp.Entries);
            listView.View = grid;
            return listView;
        }
    }
}
