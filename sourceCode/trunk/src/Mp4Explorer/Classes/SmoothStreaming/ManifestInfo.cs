//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Collections.Generic;

namespace Mp4Explorer.SmoothStreaming
{
    public sealed class ManifestInfo
    {
        public string Name { get; }
        public string Filename { get; }
        public List<StreamInfo> Streams { get; } = new List<StreamInfo>();

        public ManifestInfo(string name, string filename)
        {
            Name = name;
            Filename = filename;
        }
    }
}
