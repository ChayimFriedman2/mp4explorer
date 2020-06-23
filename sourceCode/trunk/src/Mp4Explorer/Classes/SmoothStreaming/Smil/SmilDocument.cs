//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.IO;
using System.Xml.Serialization;

namespace Mp4Explorer.SmoothStreaming.Smil
{
    [XmlRoot("smil", Namespace = "http://www.w3.org/2001/SMIL20/Language", IsNullable = false)]
    public class SmilDocument
    {
        private static readonly XmlSerializer _Serializer = new XmlSerializer(typeof(SmilDocument));

        [XmlElement("head")]
        public SmilHead Head { get; } = new SmilHead();
        [XmlElement("body")]
        public SmilBody Body { get; } = new SmilBody();

        public void Save(string filename)
        {
            using Stream stream = File.OpenWrite(filename);
            _Serializer.Serialize(stream, this);
        }

        public static SmilDocument Load(string filename)
        {
            using Stream stream = File.OpenRead(filename);
            return (SmilDocument)_Serializer.Deserialize(stream);
        }
    }
}
