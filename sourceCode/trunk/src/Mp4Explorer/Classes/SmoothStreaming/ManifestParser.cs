//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System;
using System.Globalization;
using System.IO;
using System.Xml;

namespace Mp4Explorer.SmoothStreaming
{
    public static class ManifestParser
    {
        public static ManifestInfo Parse(string filename)
        {
            var manifestInfo = new ManifestInfo(Path.GetFileNameWithoutExtension(filename), filename);
            var manifest = XmlReader.Create(filename);
            while (manifest.Read())
            {
                if (manifest.IsStartElement("StreamIndex"))
                {
                    StreamInfo streamInfo = ParseStreamInfo(manifest);
                    if (streamInfo != null)
                    {
                        manifestInfo.Streams.Add(streamInfo);
                    }
                }
            }
            return manifestInfo;
        }

        private static StreamInfo ParseStreamInfo(XmlReader manifest)
        {
            string attribute = manifest.GetAttribute("Type");
            MediaType mediaType = MediaType.Unknown;
            if (attribute.Equals("video", StringComparison.InvariantCultureIgnoreCase))
            {
                mediaType = MediaType.Video;
            }
            else if (attribute.Equals("audio", StringComparison.InvariantCultureIgnoreCase))
            {
                mediaType = MediaType.Audio;
            }

            if (mediaType == MediaType.Unknown)
            {
                throw new Exception("Stream media type in manifest may be 'audio' or 'video' only");
            }

            var streamInfo = new StreamInfo(mediaType);
            GetQualityLevels(manifest, streamInfo);
            return streamInfo;
        }

        private static void GetQualityLevels(XmlReader manifest, StreamInfo streamInfo)
        {
            while (manifest.Read())
            {
                if (manifest.IsStartElement("QualityLevel"))
                {
                    ulong bitrate = Convert.ToUInt64(manifest.GetAttribute("Bitrate"), CultureInfo.InvariantCulture);
                    streamInfo.QualityLevels.Add(new QualityLevelInfo(bitrate));
                }
                else if (manifest.IsStartElement("c"))
                {
                    int chunkId = Convert.ToInt32(manifest.GetAttribute("n"), CultureInfo.InvariantCulture);
                    ulong duration = Convert.ToUInt64(manifest.GetAttribute("d"), CultureInfo.InvariantCulture);
                    streamInfo.Chunks.Add(new MediaChunk(chunkId, duration));
                }
                else if (manifest.Name.Equals("StreamIndex"))
                {
                    break;
                }
            }
        }
    }
}
