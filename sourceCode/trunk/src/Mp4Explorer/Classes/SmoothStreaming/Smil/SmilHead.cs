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
    [XmlRoot("head")]
    public class SmilHead
    {
        [XmlElement("meta")]
        private SmilMeta[] _Meta { get; set; }
        public IEnumerable<SmilMeta> Meta => _Meta;

        public SmilHead()
        {
            _Meta = Array.Empty<SmilMeta>();
        }

        public void AddMeta(SmilMeta meta)
        {
            _Meta = new[] { meta };
        }
    }
}
