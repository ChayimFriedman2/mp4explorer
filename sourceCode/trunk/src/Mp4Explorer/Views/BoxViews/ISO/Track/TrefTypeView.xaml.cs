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
    [BoxViewType(typeof(Mp4TrefTypeBox))]
    public partial class TrefTypeView : UserControl, IBoxView
    {
        private Mp4TrefTypeBox _Box;

        public TrefTypeView()
        {
            InitializeComponent();
        }

        public Mp4Box Box
        {
            get => _Box;
            set
            {
                _Box = (Mp4TrefTypeBox)value;
                BoxViewUtil.AddHeader(grid, "Track Reference Box");
                if (_Box.Type == Mp4BoxType.HINT)
                {
                    BoxViewUtil.AddField(grid, "hint:", "The referenced track(s) contain the original media for this hint track.");
                }
                else if (_Box.Type == Mp4BoxType.CDSC)
                {
                    BoxViewUtil.AddField(grid, "cdsc:", "This track describes the referenced track.");
                }
                //TODO: Add box type.
                //else if (box.Type == Mp4BoxType.HIND)
                // BoxViewUtil.AddField(grid, "hind:", "This track depends on the referenced hint track, it should only be used if the referenced hint track is used.");
                BoxViewUtil.AddSubHeader(grid, "Tracks");
                BoxViewUtil.AddControl(grid, BuildListView(_Box)).Height = new GridLength(400, GridUnitType.Pixel);
            }
        }

        private ListView BuildListView(Mp4TrefTypeBox trefType)
        {
            var listView = new ListView();
            var grid = new GridView();
            var c1 = new GridViewColumn
            {
                Header = "ID",
            };
            grid.Columns.Add(c1);
            listView.ItemsSource = new ObservableCollection<uint>(trefType.TrackIds);
            listView.View = grid;
            return listView;
        }
    }
}
