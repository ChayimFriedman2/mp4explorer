//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CMStream.Mp4;

namespace Mp4Explorer
{
    public partial class MainView : UserControl, IMainView
    {
        private Mp4File _File;

        public MainView()
        {
            InitializeComponent();
        }

        public Mp4File File
        {
            get => _File;
            set
            {
                _File = value;
                ShowFileInfo(_File.FileType);
                Mp4Movie movie = _File.Movie;
                if (movie != null)
                {
                    ShowMovieInfo(movie);
                    ShowTracks(movie.Tracks, false, true);
                }
            }
        }

        public void ShowFileInfo(Mp4FtypBox fileType)
        {
            if (fileType == null)
            {
                return;
            }
            textBoxMajorBrand.Text = Mp4Util.FormatFourChars(fileType.MajorBrand);
            textBoxMinorVersion.Text = fileType.MinorVersion.ToString();
            textBoxCompatibleBrands.Text = Mp4Util.FormatFourChars(fileType.CompatibleBrands);
        }

        public void ShowMovieInfo(Mp4Movie movie)
        {
            textBoxDurationMs.Text = movie.DurationMs.ToString();
            textBoxTimeScale.Text = movie.TimeScale.ToString();
            textBoxTracks.Text = movie.Tracks.Count.ToString();
        }

        public void ShowTracks(List<Mp4Track> tracks, bool showSamples, bool verbose)
        {
            _ = showSamples;
            _ = verbose;

            int index = 1;
            foreach (Mp4Track track in tracks)
            {
                ShowTrackInfo(index, track);
                index++;
            }
        }

        public void ShowTrackInfo(int trackIndex, Mp4Track track)
        {
            var expander = new Expander
            {
                Header = string.Format("Track {0}", trackIndex),
                IsExpanded = true,
                Background = Brushes.LightBlue,
            };
            var grid = new Grid
            {
                Background = Brushes.White,
            };
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            var label = new Label
            {
                HorizontalContentAlignment = HorizontalAlignment.Right,
                Content = "Flags:",
            };
            var textBox = new TextBox
            {
                IsReadOnly = true,
                Text = track.Flags.ToString(),
            };
            if ((track.Flags & Mp4Track.TRACK_FLAG_ENABLED) != 0)
            {
                textBox.Text += " ENABLED";
            }
            if ((track.Flags & Mp4Track.TRACK_FLAG_IN_MOVIE) != 0)
            {
                textBox.Text += " IN-MOVIE";
            }
            if ((track.Flags & Mp4Track.TRACK_FLAG_IN_PREVIEW) != 0)
            {
                textBox.Text += " IN-PREVIEW";
            }
            Grid.SetColumn(label, 0);
            Grid.SetRow(label, grid.RowDefinitions.Count - 1);
            grid.Children.Add(label);
            Grid.SetColumn(textBox, 1);
            Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
            grid.Children.Add(textBox);
            grid.RowDefinitions.Add(new RowDefinition());
            label = new Label
            {
                HorizontalContentAlignment = HorizontalAlignment.Right,
                Content = "Id:",
            };
            textBox = new TextBox
            {
                IsReadOnly = true,
                Text = track.Id.ToString()
            };
            Grid.SetColumn(label, 0);
            Grid.SetRow(label, grid.RowDefinitions.Count - 1);
            grid.Children.Add(label);
            Grid.SetColumn(textBox, 1);
            Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
            grid.Children.Add(textBox);
            grid.RowDefinitions.Add(new RowDefinition());
            label = new Label
            {
                HorizontalContentAlignment = HorizontalAlignment.Right,
                Content = "Type:",
            };
            textBox = new TextBox
            {
                IsReadOnly = true,
            };
            switch (track.Type)
            {
                case Mp4TrackType.AUDIO: textBox.Text = "Audio"; break;
                case Mp4TrackType.VIDEO: textBox.Text = "Video"; break;
                case Mp4TrackType.HINT: textBox.Text = "Hint"; break;
                case Mp4TrackType.SYSTEM: textBox.Text = "System"; break;
                case Mp4TrackType.TEXT: textBox.Text = "Text"; break;
                case Mp4TrackType.JPEG: textBox.Text = "Jpeg"; break;
                default:
                    string hdlr = Mp4Util.FormatFourChars(track.HandlerType);
                    textBox.Text = $"Unknown [{hdlr}]";
                    break;
            }
            Grid.SetColumn(label, 0);
            Grid.SetRow(label, grid.RowDefinitions.Count - 1);
            grid.Children.Add(label);
            Grid.SetColumn(textBox, 1);
            Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
            grid.Children.Add(textBox);
            grid.RowDefinitions.Add(new RowDefinition());
            label = new Label
            {
                HorizontalContentAlignment = HorizontalAlignment.Right,
                Content = "Duration(ms):",
            };
            textBox = new TextBox
            {
                IsReadOnly = true,
                Text = Mp4Util.ConvertTime(track.Duration, track.MediaTimeScale, 1000).ToString(),
            };
            Grid.SetColumn(label, 0);
            Grid.SetRow(label, grid.RowDefinitions.Count - 1);
            grid.Children.Add(label);
            Grid.SetColumn(textBox, 1);
            Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
            grid.Children.Add(textBox);
            grid.RowDefinitions.Add(new RowDefinition());
            label = new Label
            {
                HorizontalContentAlignment = HorizontalAlignment.Right,
                Content = "Time scale:",
            };
            textBox = new TextBox
            {
                IsReadOnly = true,
                Text = track.MediaTimeScale.ToString(),
            };
            Grid.SetColumn(label, 0);
            Grid.SetRow(label, grid.RowDefinitions.Count - 1);
            grid.Children.Add(label);
            Grid.SetColumn(textBox, 1);
            Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
            grid.Children.Add(textBox);
            grid.RowDefinitions.Add(new RowDefinition());
            label = new Label
            {
                HorizontalContentAlignment = HorizontalAlignment.Right,
                Content = "Sample count:",
            };
            textBox = new TextBox
            {
                IsReadOnly = true,
                Text = track.SampleCount.ToString(),
            };
            Grid.SetColumn(label, 0);
            Grid.SetRow(label, grid.RowDefinitions.Count - 1);
            grid.Children.Add(label);
            Grid.SetColumn(textBox, 1);
            Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
            grid.Children.Add(textBox);
            Mp4Sample sample;
            int index = 0;
            uint descIndex = 0xFFFFFFFF;
            if (track.SampleCount > 0)
            {
                while ((sample = track.GetSample(index)) != null)
                {
                    if (sample.DescriptionIndex != descIndex)
                    {
                        descIndex = sample.DescriptionIndex;
                        // get the sample description
                        Mp4SampleEntry sample_desc = track.GetSampleDescription((int)descIndex);
                        if (sample_desc != null)
                        {
                            ShowSampleDescription(grid, (int)descIndex, sample_desc);
                        }
                        break;
                    }
                    index++;
                }
            }
            else
            {
                for (int i = 0; i < track.SampleDescriptionCount; i++)
                {
                    Mp4SampleEntry sampleDescription = track.GetSampleDescription(i);
                    if (sampleDescription != null)
                    {
                        ShowSampleDescription(grid, i, sampleDescription);
                    }
                }
            }
            expander.Content = grid;
            stackPanel.Children.Add(expander);
        }

        public static void ShowSampleDescription(Grid grid, int sampleDescIndex, Mp4SampleEntry description)
        {
            grid.RowDefinitions.Add(new RowDefinition());
            var label = new Label
            {
                Background = Brushes.LightGray,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Content = "Sample Description " + sampleDescIndex,
            };
            Grid.SetColumn(label, 0);
            Grid.SetRow(label, grid.RowDefinitions.Count - 1);
            Grid.SetColumnSpan(label, 2);
            grid.Children.Add(label);
            Mp4SampleEntry desc = description;
            //if (desc is Mp4ProtectedSampleEntry protDesc)
            //{
            //    ShowProtectedSampleDescription(*protDesc, verbose);
            //    desc = protDesc->GetOriginalSampleDescription();
            //}
            string coding = Mp4Util.FormatFourChars(desc.Type);
            grid.RowDefinitions.Add(new RowDefinition());
            label = new Label
            {
                HorizontalContentAlignment = HorizontalAlignment.Right,
                Content = "Coding:",
            };
            var textBox = new TextBox
            {
                IsReadOnly = true,
                Text = coding,
            };
            Grid.SetColumn(label, 0);
            Grid.SetRow(label, grid.RowDefinitions.Count - 1);
            grid.Children.Add(label);
            Grid.SetColumn(textBox, 1);
            Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
            grid.Children.Add(textBox);
            // MPEG sample description
            if (desc is Mp4MpegVideoSampleEntry || desc is Mp4MpegAudioSampleEntry)
            {
                Mp4EsdsBox esds = desc.GetChild<Mp4EsdsBox>(Mp4BoxType.ESDS);
                if (esds?.EsDescriptor != null)
                {
                    Mp4DecoderConfigDescriptor dcd = esds.EsDescriptor.DecoderConfigDescriptor;
                    if (dcd != null)
                    {
                        grid.RowDefinitions.Add(new RowDefinition());
                        label = new Label
                        {
                            HorizontalContentAlignment = HorizontalAlignment.Right,
                            Content = "Stream Type:",
                        };
                        textBox = new TextBox
                        {
                            IsReadOnly = true,
                            Text = Mp4Util.GetStreamTypeString((Mp4StreamType)dcd.StreamType),
                        };
                        Grid.SetColumn(label, 0);
                        Grid.SetRow(label, grid.RowDefinitions.Count - 1);
                        grid.Children.Add(label);
                        Grid.SetColumn(textBox, 1);
                        Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
                        grid.Children.Add(textBox);
                        grid.RowDefinitions.Add(new RowDefinition());
                        label = new Label
                        {
                            HorizontalContentAlignment = HorizontalAlignment.Right,
                            Content = "Object Type:",
                        };
                        textBox = new TextBox
                        {
                            IsReadOnly = true,
                            Text = Mp4Util.GetObjectTypeString(dcd.ObjectTypeIndication),
                        };
                        Grid.SetColumn(label, 0);
                        Grid.SetRow(label, grid.RowDefinitions.Count - 1);
                        grid.Children.Add(label);
                        Grid.SetColumn(textBox, 1);
                        Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
                        grid.Children.Add(textBox);
                        grid.RowDefinitions.Add(new RowDefinition());
                        label = new Label
                        {
                            HorizontalContentAlignment = HorizontalAlignment.Right,
                            Content = "Max Bitrate:",
                        };
                        textBox = new TextBox
                        {
                            IsReadOnly = true,
                            Text = dcd.MaxBitrate.ToString(),
                        };
                        Grid.SetColumn(label, 0);
                        Grid.SetRow(label, grid.RowDefinitions.Count - 1);
                        grid.Children.Add(label);
                        Grid.SetColumn(textBox, 1);
                        Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
                        grid.Children.Add(textBox);
                        grid.RowDefinitions.Add(new RowDefinition());
                        label = new Label
                        {
                            HorizontalContentAlignment = HorizontalAlignment.Right,
                            Content = "Avg Bitrate:",
                        };
                        textBox = new TextBox
                        {
                            IsReadOnly = true,
                            Text = dcd.AverageBitrate.ToString(),
                        };
                        Grid.SetColumn(label, 0);
                        Grid.SetRow(label, grid.RowDefinitions.Count - 1);
                        grid.Children.Add(label);
                        Grid.SetColumn(textBox, 1);
                        Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
                        grid.Children.Add(textBox);
                        grid.RowDefinitions.Add(new RowDefinition());
                        label = new Label
                        {
                            HorizontalContentAlignment = HorizontalAlignment.Right,
                            Content = "Buffer Size:",
                        };
                        textBox = new TextBox
                        {
                            IsReadOnly = true,
                            Text = dcd.BufferSize.ToString(),
                        };
                        Grid.SetColumn(label, 0);
                        Grid.SetRow(label, grid.RowDefinitions.Count - 1);
                        grid.Children.Add(label);
                        Grid.SetColumn(textBox, 1);
                        Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
                        grid.Children.Add(textBox);
                    }
                    if (desc is Mp4MpegAudioSampleEntry audioDesc2)
                    {
                        grid.RowDefinitions.Add(new RowDefinition());
                        label = new Label
                        {
                            HorizontalContentAlignment = HorizontalAlignment.Right,
                            Content = "MPEG-4 Audio Object Type:",
                        };
                        textBox = new TextBox
                        {
                            IsReadOnly = true,
                            Text = Mp4Util.GetMpeg4AudioObjectTypeString(audioDesc2.Mpeg4AudioObjectType),
                        };
                        Grid.SetColumn(label, 0);
                        Grid.SetRow(label, grid.RowDefinitions.Count - 1);
                        grid.Children.Add(label);
                        Grid.SetColumn(textBox, 1);
                        Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
                        grid.Children.Add(textBox);
                    }
                }
            }
            // AVC Sample Description
            if (desc is Mp4Avc1SampleEntry avcDesc)
            {
                string profile_name = Mp4Util.GetProfileName(avcDesc.Profile) ?? avcDesc.Profile.ToString();
                grid.RowDefinitions.Add(new RowDefinition());
                label = new Label
                {
                    HorizontalContentAlignment = HorizontalAlignment.Right,
                    Content = "AVC Profile:",
                };
                textBox = new TextBox
                {
                    IsReadOnly = true,
                    Text = profile_name,
                };
                Grid.SetColumn(label, 0);
                Grid.SetRow(label, grid.RowDefinitions.Count - 1);
                grid.Children.Add(label);
                Grid.SetColumn(textBox, 1);
                Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
                grid.Children.Add(textBox);
                grid.RowDefinitions.Add(new RowDefinition());
                label = new Label
                {
                    HorizontalContentAlignment = HorizontalAlignment.Right,
                    Content = "AVC Profile Compat:",
                };
                textBox = new TextBox
                {
                    IsReadOnly = true,
                    Text = avcDesc.ProfileCompatibility.ToString("X"),
                };
                Grid.SetColumn(label, 0);
                Grid.SetRow(label, grid.RowDefinitions.Count - 1);
                grid.Children.Add(label);
                Grid.SetColumn(textBox, 1);
                Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
                grid.Children.Add(textBox);
                grid.RowDefinitions.Add(new RowDefinition());
                label = new Label
                {
                    HorizontalContentAlignment = HorizontalAlignment.Right,
                    Content = "AVC Level:",
                };
                textBox = new TextBox
                {
                    IsReadOnly = true,
                    Text = avcDesc.Level.ToString(),
                };
                Grid.SetColumn(label, 0);
                Grid.SetRow(label, grid.RowDefinitions.Count - 1);
                grid.Children.Add(label);
                Grid.SetColumn(textBox, 1);
                Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
                grid.Children.Add(textBox);
                grid.RowDefinitions.Add(new RowDefinition());
                label = new Label
                {
                    HorizontalContentAlignment = HorizontalAlignment.Right,
                    Content = "AVC NALU Length Size:",
                };
                textBox = new TextBox
                {
                    IsReadOnly = true,
                    Text = avcDesc.NaluLengthSize.ToString(),
                };
                Grid.SetColumn(label, 0);
                Grid.SetRow(label, grid.RowDefinitions.Count - 1);
                grid.Children.Add(label);
                Grid.SetColumn(textBox, 1);
                Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
                grid.Children.Add(textBox);
            }
            if (desc is Mp4AudioSampleEntry audioDesc)
            {
                // Audio sample description
                grid.RowDefinitions.Add(new RowDefinition());
                label = new Label
                {
                    HorizontalContentAlignment = HorizontalAlignment.Right,
                    Content = "Sample Rate:"
                };
                textBox = new TextBox
                {
                    IsReadOnly = true,
                    Text = Mp4Util.FormatDouble(audioDesc.SampleRate)
                };
                Grid.SetColumn(label, 0);
                Grid.SetRow(label, grid.RowDefinitions.Count - 1);
                grid.Children.Add(label);
                Grid.SetColumn(textBox, 1);
                Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
                grid.Children.Add(textBox);
                grid.RowDefinitions.Add(new RowDefinition());
                label = new Label
                {
                    HorizontalContentAlignment = HorizontalAlignment.Right,
                    Content = "Sample Size:",
                };
                textBox = new TextBox
                {
                    IsReadOnly = true,
                    Text = audioDesc.SampleSize.ToString(),
                };
                Grid.SetColumn(label, 0);
                Grid.SetRow(label, grid.RowDefinitions.Count - 1);
                grid.Children.Add(label);
                Grid.SetColumn(textBox, 1);
                Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
                grid.Children.Add(textBox);
                grid.RowDefinitions.Add(new RowDefinition());
                label = new Label
                {
                    HorizontalContentAlignment = HorizontalAlignment.Right,
                    Content = "Channels:",
                };
                textBox = new TextBox
                {
                    IsReadOnly = true,
                    Text = audioDesc.ChannelCount.ToString(),
                };
                Grid.SetColumn(label, 0);
                Grid.SetRow(label, grid.RowDefinitions.Count - 1);
                grid.Children.Add(label);
                Grid.SetColumn(textBox, 1);
                Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
                grid.Children.Add(textBox);
            }
            if (desc is Mp4VisualSampleEntry videoDesc)
            {
                // Video sample description
                grid.RowDefinitions.Add(new RowDefinition());
                label = new Label
                {
                    HorizontalContentAlignment = HorizontalAlignment.Right,
                    Content = "Width:",
                };
                textBox = new TextBox
                {
                    IsReadOnly = true,
                    Text = videoDesc.Width.ToString(),
                };
                Grid.SetColumn(label, 0);
                Grid.SetRow(label, grid.RowDefinitions.Count - 1);
                grid.Children.Add(label);
                Grid.SetColumn(textBox, 1);
                Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
                grid.Children.Add(textBox);
                grid.RowDefinitions.Add(new RowDefinition());
                label = new Label
                {
                    HorizontalContentAlignment = HorizontalAlignment.Right,
                    Content = "Height:",
                };
                textBox = new TextBox
                {
                    IsReadOnly = true,
                    Text = videoDesc.Height.ToString(),
                };
                Grid.SetColumn(label, 0);
                Grid.SetRow(label, grid.RowDefinitions.Count - 1);
                grid.Children.Add(label);
                Grid.SetColumn(textBox, 1);
                Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
                grid.Children.Add(textBox);
                grid.RowDefinitions.Add(new RowDefinition());
                label = new Label
                {
                    HorizontalContentAlignment = HorizontalAlignment.Right,
                    Content = "Depth:",
                };
                textBox = new TextBox
                {
                    IsReadOnly = true,
                    Text = videoDesc.Depth.ToString(),
                };
                Grid.SetColumn(label, 0);
                Grid.SetRow(label, grid.RowDefinitions.Count - 1);
                grid.Children.Add(label);
                Grid.SetColumn(textBox, 1);
                Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
                grid.Children.Add(textBox);
            }
        }
    }
}
