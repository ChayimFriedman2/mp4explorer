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
    [XmlRoot("audio")]
    public sealed class SmilAudio : ISmilStream
    {
        [XmlAttribute("src")]
        public string Src { get; set; }
        [XmlAttribute("systemBitrate")]
        public string SystemBitrate { get; set; }
        [XmlElement("param")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1819:Properties should not return arrays")]
        public SmilParam[] Param { get; set; }
        IEnumerable<SmilParam> ISmilStream.Param => Param;

        public SmilAudio()
        {
            Param = Array.Empty<SmilParam>();
        }

        public SmilAudio(string src, string systemBitrate)
        {
            Src = src;
            SystemBitrate = systemBitrate;
            Param = Array.Empty<SmilParam>();
        }

        public void AddParam(SmilParam param)
        {
            Param = new[] { param };
        }
    }
}
