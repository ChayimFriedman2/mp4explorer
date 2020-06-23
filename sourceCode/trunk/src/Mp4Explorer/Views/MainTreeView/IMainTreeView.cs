//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System;
using System.Windows.Controls;
using System.Windows.Threading;
using Prism.Events;

namespace Mp4Explorer
{
    public interface IMainTreeView
    {
        TreeViewItem RootNode { get; set; }
        Dispatcher Dispatcher { get; }
        event EventHandler<DataEventArgs<TreeViewItem>> NodeSelected;
    }
}
