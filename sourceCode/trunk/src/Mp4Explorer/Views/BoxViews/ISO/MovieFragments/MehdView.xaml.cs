//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Windows.Controls;
using CMStream.Mp4;

namespace Mp4Explorer
{
    [BoxViewType(typeof(Mp4MehdBox))]
    public partial class MehdView : UserControl, IBoxView
    {
        private Mp4MehdBox _Box;

        public MehdView()
        {
            InitializeComponent();
        }

        public Mp4Box Box
        {
            get => _Box;
            set
            {
                _Box = (Mp4MehdBox)value;
                BoxViewUtil.AddHeader(grid, "Movie Extends Header Box");
                BoxViewUtil.AddField(grid, "Fragment duration:", _Box.FragmentDuration);
            }
        }
    }
}
