//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Xml.Serialization;

namespace Mp4Explorer.SmoothStreaming.Smil
{
    [XmlRoot("param")]
    public class SmilParam
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("value")]
        public string Value { get; set; }
        [XmlAttribute("valuetype")]
        public string ValueType { get; set; }

        public SmilParam(string name, string value, string valueType)
        {
            Name = name;
            Value = value;
            ValueType = valueType;
        }
    }
}
