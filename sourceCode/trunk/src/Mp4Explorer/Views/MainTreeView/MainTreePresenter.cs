//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using CMStream.Mp4;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Events;

namespace Mp4Explorer
{
    public class MainTreePresenter : IMainTreePresenter
    {
        private readonly IMainController _Controller;
        private readonly GlobalCommandsProxy _CommandsProxy;
        private Mp4File _File;
        private Mp4Stream _Stream;

        public IMainTreeView View { get; set; }
        public DelegateCommand<object> OpenCommand { get; }

        public MainTreePresenter(IMainTreeView view, IMainService _, IMainController controller, GlobalCommandsProxy commandsProxy)
        {
            _Controller = controller;
            _CommandsProxy = commandsProxy;
            View = view;
            View.NodeSelected += View_NodeSelected;
            OpenCommand = new DelegateCommand<object>(Open);
            _CommandsProxy.OpenCommand.RegisterCommand(OpenCommand);
        }

        private void View_NodeSelected(object sender, DataEventArgs<TreeViewItem> e)
        {
            if (e.Value != null && e.Value is Mp4TreeNode node)
            {
                _Controller.OnBoxSelected(node.Box);
            }
            else
            {
                _Controller.ShowFile(_File);
            }
        }

        private void Open(object obj)
        {
            try
            {
                var openFileDialog = new OpenFileDialog
                {
                    Filter = "MP4 Files (*.mp4,*.m4a,*.ismv,*.mov)|*.mp4;*.m4a;*.ismv;*.mov|All Files (*.*)|*.*",
                    FilterIndex = 1
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    _Stream?.Close();
                    var backgroundWorker = new BackgroundWorker();
                    Exception exceptionError = null;
                    backgroundWorker.DoWork += (object s, DoWorkEventArgs args) =>
                    {
                        try
                        {
                            _Stream = new Mp4Stream(openFileDialog.OpenFile());
                            _File = new Mp4File(_Stream);
                            List<Mp4Box> unknowBoxes = _File.Boxes.FindAll(b => b is Mp4UnknownBox);
                            if (unknowBoxes.Count > 1)
                            {
                                throw new Exception("Too many unknown boxes.");
                            }
                            View.Dispatcher.Invoke(new Action(() =>
                            {
                                View.RootNode = BuildTree(_File, Path.GetFileName(openFileDialog.FileName));
                                _Controller.RemoveViews();
                            }));
                        }
                        catch (Exception ex)
                        {
                            exceptionError = ex;
                        }
                    };
                    var progressWindow = new ProgressWindow(backgroundWorker);
                    progressWindow.ShowDialog();
                    if (exceptionError != null)
                    {
                        throw exceptionError;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Error loading mp4 file", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private TreeViewItem BuildTree(Mp4File file, string name)
        {
            var root = new ImageTreeViewItem
            {
                Text = name,
                ImageSource = "/Mp4Explorer;component/Images/movie_16.ico",
            };
            foreach (Mp4Box box in file.Boxes)
            {
                root.Items.Add(new Mp4TreeNode(box));
            }
            return root;
        }
    }
}
