//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Mp4Explorer.SmoothStreaming.Smil
{
    [XmlRoot("switch")]
    public class SmilSwitch
    {
        [XmlElement("video")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1819:Properties should not return arrays")]
        public SmilVideo[] Video { get; set; }
        [XmlElement("audio")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1819:Properties should not return arrays")]
        public SmilAudio[] Audio { get; set; }

        public SmilSwitch()
        {
            Video = Array.Empty<SmilVideo>();
            Audio = Array.Empty<SmilAudio>();
        }

        public void AddVideo(SmilVideo video)
        {
            Video = new[] { video };
        }
        public void AddAudio(SmilAudio audio)
        {
            Audio = new[] { audio };
        }
    }
}
