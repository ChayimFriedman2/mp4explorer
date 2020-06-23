//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System;
using System.Windows;
using System.Windows.Controls;
using Prism.Events;

namespace Mp4Explorer
{
    public partial class MainTreeView : UserControl, IMainTreeView
    {
        public event EventHandler<DataEventArgs<TreeViewItem>> NodeSelected;

        private TreeViewItem _RootNode;

        public MainTreeView()
        {
            InitializeComponent();
        }

        public TreeViewItem RootNode
        {
            get => _RootNode;
            set
            {
                TreeView.Items.Clear();
                _RootNode = value;
                if (_RootNode != null)
                {
                    TreeView.Items.Add(_RootNode);
                }
            }
        }

        private void TreeView_Selected(object sender, RoutedEventArgs e)
        {
            NodeSelected?.Invoke(this, new DataEventArgs<TreeViewItem>(e.Source as TreeViewItem));
        }
    }
}
