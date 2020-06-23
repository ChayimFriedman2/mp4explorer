//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Win32;
using Mp4Explorer.SmoothStreaming;

namespace Mp4Explorer
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "RCS1090:Call 'ConfigureAwait(false)'.")]
    public partial class UploadToAzureWindow : Window
    {
        private Exception _DoWorkException = null;
        private string _Account = null;
        private string _Key = null;
        private string _ContainerName = null;

        private string _FovFilename;
        private ManifestInfo _ManifestInfo;

        public UploadToAzureWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            buttonUpload.IsEnabled = false;
        }

        private void ButtonBrowseFov_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var openFileDialog = new OpenFileDialog
                {
                    Filter = "Fixed Offset Files (*.fov)|*.fov|All Files (*.*)|*.*",
                    FilterIndex = 1
                };
                if (openFileDialog.ShowDialog() == true)
                {
                    _FovFilename = openFileDialog.FileName;
                    string directoryName = new FileInfo(_FovFilename).Directory.FullName;
                    string manifestFilename = Path.Combine(directoryName, Path.GetFileNameWithoutExtension(_FovFilename) + ".ismc");
                    _ManifestInfo = ManifestParser.Parse(manifestFilename);
                    textBoxFovPath.Text = _FovFilename;
                    textBoxFovPath.ToolTip = _FovFilename;
                    buttonUpload.IsEnabled = true;
                }
                else
                {
                    buttonUpload.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                buttonUpload.IsEnabled = false;
                var errorWin = new ErrorWindow("Error loading video data.", ex)
                {
                    Owner = this
                };
                errorWin.ShowDialog();
            }
        }

        private void ButtonButtonUpload_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _DoWorkException = null;
                var backgroundWorker = new BackgroundWorker();
                _Account = textBoxStorateAccount.Text.Trim();
                _Key = passwordBoxSharedKey.Password.Trim();
                _ContainerName = textBoxContainerName.Text.Trim();
                backgroundWorker.DoWork += Upload_DoWork;
                var progressWindow = new ProgressWindow(backgroundWorker)
                {
                    Owner = this,
                };
                progressWindow.ShowDialog();
                if (_DoWorkException == null)
                {
                    MessageBox.Show("Video uploaded to Azure Storage account.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    throw _DoWorkException;
                }
            }
            catch (Exception ex)
            {
                var errorWin = new ErrorWindow("Error uploading to azure.", ex)
                {
                    Owner = this
                };
                errorWin.ShowDialog();
            }
        }

        private async void Upload_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string connectionString = $@"DefaultEndpointsProtocol=https;
AccountName={_Account};
AccountKey={_Key};
BlobEndpoint=https://{_Account}.blob.core.windows.net;
TableEndpoint=https://{_Account}.blob.core.windows.net;
QueueEndpoint=https://{_Account}.blob.core.windows.net;";
                var client = new BlobServiceClient(connectionString);
                BlobContainerClient container = (await client.CreateBlobContainerAsync(_ContainerName)).Value;
                await container.CreateIfNotExistsAsync(PublicAccessType.BlobContainer);
                await container.SetAccessPolicyAsync(PublicAccessType.BlobContainer);
                string directoryName = Path.GetDirectoryName(_FovFilename);
                string blobName = $"{_ContainerName}/{_ManifestInfo.Name}.ism/Manifest";
                await container.DeleteBlobIfExistsAsync(blobName);

                using (var manifest = File.OpenRead(_ManifestInfo.Filename))
                {
                    await container.UploadBlobAsync(blobName, manifest);
                }

                await Task.WhenAll(File.ReadAllLines(_FovFilename).Select(async line =>
                {
                    string[] arr = line.Split(',');
                    string mediatype = arr[0].Split('=')[1];
                    string bitrate = arr[1].Split('=')[1];
                    string starttime = arr[2].Split('=')[1];
                    string file = arr[3].Split('=')[1];
                    string offset = arr[4].Split('=')[1];
                    string size = arr[5].Split('=')[1];
                    string path = $"{_ContainerName}/{_ManifestInfo.Name}.ism/QualityLevels({bitrate})/Fragments({mediatype}={starttime})";
                    using var stream = File.OpenRead(Path.Combine(directoryName, file));
                    stream.Position = int.Parse(offset);
                    stream.SetLength(stream.Position + int.Parse(size));
                    await container.DeleteBlobIfExistsAsync(path);
                    await container.UploadBlobAsync(path, stream);
                }));
            }
            catch (Exception ex)
            {
                _DoWorkException = ex;
            }
        }
    }
}
