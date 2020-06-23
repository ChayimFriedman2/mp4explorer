//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Xml.Serialization;

namespace Mp4Explorer.SmoothStreaming.Smil
{
    [XmlRoot("meta")]
    public class SmilMeta
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("content")]
        public string Content { get; set; }

        public SmilMeta(string name, string content)
        {
            Name = name;
            Content = content;
        }
    }
}
