//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Xml.Serialization;

namespace Mp4Explorer.SmoothStreaming.Smil
{
    [XmlRoot("body")]
    public class SmilBody
    {
        [XmlElement("switch")]
        public SmilSwitch Switch { get; } = new SmilSwitch();
    }
}
