//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using CMStream.Mp4;
using Microsoft.Win32;
using Mp4Explorer.SmoothStreaming;
using Mp4Explorer.SmoothStreaming.Smil;

namespace Mp4Explorer
{
    public partial class CreateFixedOffsetWindow : Window
    {
        private readonly SaveFileDialog _CalculateManifestSaveFileDialog = new SaveFileDialog
        {
            Filter = "Fixed offset video file (*.fov)|*.fov",
            FilterIndex = 1,
        };
        private readonly OpenFileDialog _BrowseManifestOpenFileDialog = new OpenFileDialog
        {
            Filter = "Manifest Files (*.ismc)|*.ismc|All Files (*.*)|*.*",
            FilterIndex = 1
        };
        private Exception _DoWorkException = null;

        private ManifestInfo _ManifestInfo;
        private SmilDocument _SmilDocument;

        public CreateFixedOffsetWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            labelStatus.Content = string.Empty;
            buttonCalculate.IsEnabled = false;
        }

        private void ButtonBrowseManifest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_BrowseManifestOpenFileDialog.ShowDialog() == true)
                {
                    _DoWorkException = null;
                    var backgroundWorker = new BackgroundWorker();
                    backgroundWorker.DoWork += BrowseManifest_DoWork;
                    var progressWindow = new ProgressWindow(backgroundWorker)
                    {
                        Owner = this,
                    };
                    progressWindow.ShowDialog();
                    if (_DoWorkException == null)
                    {
                        labelStatus.Content = "OK";
                        labelStatus.Background = Brushes.LightGreen;
                        buttonCalculate.IsEnabled = true;
                    }
                    else
                    {
                        labelStatus.Content = "Error";
                        labelStatus.Background = Brushes.Red;
                        buttonCalculate.IsEnabled = false;
                        throw _DoWorkException;
                    }
                }
            }
            catch (Exception ex)
            {
                var errorWin = new ErrorWindow("Error reading the video set of files.", ex)
                {
                    Owner = this
                };
                errorWin.ShowDialog();
            }
        }

        private void ButtonButtonCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _CalculateManifestSaveFileDialog.FileName = Path.Combine(Path.GetDirectoryName(_ManifestInfo.Filename), _ManifestInfo.Name + ".fov");
                if (_CalculateManifestSaveFileDialog.ShowDialog() == true)
                {
                    _DoWorkException = null;
                    var backgroundWorker = new BackgroundWorker();
                    backgroundWorker.DoWork += Calculate_DoWork;
                    var progressWindow = new ProgressWindow(backgroundWorker)
                    {
                        Owner = this,
                    };
                    progressWindow.ShowDialog();
                    if (_DoWorkException == null)
                    {
                        MessageBox.Show("Fixed offset file created.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        throw _DoWorkException;
                    }
                }
            }
            catch (Exception ex)
            {
                var errorWindow = new ErrorWindow("Error reading the video set of files.", ex)
                {
                    Owner = this
                };
                errorWindow.ShowDialog();
            }
        }

        private void BrowseManifest_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string fileName = _BrowseManifestOpenFileDialog.FileName;
                string directoryName = Path.GetDirectoryName(fileName);
                string smilFilename = Path.Combine(directoryName, Path.GetFileNameWithoutExtension(fileName) + ".ism");
                _ManifestInfo = ManifestParser.Parse(fileName);
                _SmilDocument = SmilDocument.Load(smilFilename);

                StreamInfo videoStream = _ManifestInfo.Streams.Find(stream => stream.MediaType == MediaType.Video);
                foreach (QualityLevelInfo qualityLevel in videoStream.QualityLevels)
                {
                    SmilVideo smilVideo = _SmilDocument.Body.Switch.Video.First(stream => stream.SystemBitrate == qualityLevel.Bitrate.ToString());
                    qualityLevel.TrackId = Convert.ToInt32(smilVideo.Param.First(p => p.Name == "trackID").Value, CultureInfo.InvariantCulture);
                    qualityLevel.Filename = Path.Combine(directoryName, smilVideo.Src);
                    if (!File.Exists(qualityLevel.Filename))
                    {
                        throw new FileNotFoundException("Video file not found", qualityLevel.Filename);
                    }
                }

                StreamInfo audioStream = _ManifestInfo.Streams.Find(stream => stream.MediaType == MediaType.Audio);
                foreach (QualityLevelInfo qualityLevel in audioStream.QualityLevels)
                {
                    SmilAudio smilAudio = _SmilDocument.Body.Switch.Audio.First(stream => stream.SystemBitrate == qualityLevel.Bitrate.ToString());
                    qualityLevel.TrackId = Convert.ToInt32(smilAudio.Param.First(p => p.Name == "trackID").Value, CultureInfo.InvariantCulture);
                    qualityLevel.Filename = Path.Combine(directoryName, smilAudio.Src);
                    if (!File.Exists(qualityLevel.Filename))
                    {
                        throw new FileNotFoundException("Video file not found", qualityLevel.Filename);
                    }
                }

                Dispatcher.Invoke(new Action(() =>
                {
                    textBoxManifestPath.Text = fileName;
                    textBoxManifestPath.ToolTip = fileName;
                    var grid = new GridView();
                    var c1 = new GridViewColumn
                    {
                        DisplayMemberBinding = new Binding("Type"),
                        Header = "Type",
                    };
                    grid.Columns.Add(c1);
                    var c2 = new GridViewColumn
                    {
                        DisplayMemberBinding = new Binding("File"),
                        Header = "File",
                    };
                    grid.Columns.Add(c2);
                    var c3 = new GridViewColumn
                    {
                        DisplayMemberBinding = new Binding("Path"),
                        Header = "Path",
                    };
                    grid.Columns.Add(c3);
                    var coll = new ObservableCollection<object>
                    {
                        new
                        {
                            Type = "SMIL",
                            File = Path.GetFileName(smilFilename),
                            Path = smilFilename,
                        },
                        new
                        {
                            Type = "Manifest",
                            File = Path.GetFileName(fileName),
                            Path = fileName,
                        },
                    };
                    foreach (QualityLevelInfo qualityLevel in videoStream.QualityLevels)
                    {
                        coll.Add(new
                        {
                            Type = "Quality level",
                            File = Path.GetFileNameWithoutExtension(qualityLevel.Filename),
                            Path = qualityLevel.Filename,
                        });
                    }
                    listViewFiles.ItemsSource = coll;
                    listViewFiles.View = grid;
                }));
            }
            catch (Exception ex)
            {
                _DoWorkException = ex;
            }
        }

        private void Calculate_DoWork(object s, DoWorkEventArgs args)
        {
            try
            {
                string fileName = _CalculateManifestSaveFileDialog.FileName;
                using var output = new StreamWriter(fileName, false, Encoding.UTF8);
                foreach (StreamInfo streamInfo in _ManifestInfo.Streams)
                {
                    foreach (QualityLevelInfo qualityLevel in streamInfo.QualityLevels)
                    {
                        var stream = new Mp4FileStream(qualityLevel.Filename, FileAccess.Read);
                        try
                        {
                            Mp4MfraBox mfra = GetMfra(stream);
                            var tfra = mfra.Children.OfType<Mp4TfraBox>()
                                .First(b => b.TrackId == qualityLevel.TrackId);
                            foreach (MediaChunk chunk in streamInfo.Chunks)
                            {
                                Mp4TfraEntry entry = tfra.Entries[chunk.ChunkId];
                                stream.Position = (long)entry.MoofOffset;
                                uint size = 0;
                                // moof
                                size = stream.ReadUInt32();
                                stream.Position += size - 4;
                                // mdat
                                size += stream.ReadUInt32();
                                string line = string.Format("mediatype={0},bitrate={1},starttime={2},file={3},offset={4},size={5}", streamInfo.MediaType.ToString().ToLower(), qualityLevel.Bitrate, entry.Time, Path.GetFileName(qualityLevel.Filename), entry.MoofOffset, size);
                                output.WriteLine(line);
                            }
                        }
                        finally
                        {
                            stream.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _DoWorkException = ex;
            }
        }

        /// <summary>
        /// Read Mfra box.
        /// </summary>
        /// <param name="stream">Mp4 stream.</param>
        /// <returns>Mfra box.</returns>
        private Mp4MfraBox GetMfra(Mp4Stream stream)
        {
            stream.Position = stream.Length - 4;
            uint mfraSize = stream.ReadUInt32();
            stream.Position = stream.Length - mfraSize;
            var file = new Mp4File(stream);
            return (Mp4MfraBox)file.Boxes[0];
        }
    }
}
