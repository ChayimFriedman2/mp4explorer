//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Collections.Generic;

namespace Mp4Explorer.SmoothStreaming
{
    public sealed class StreamInfo
    {
        public MediaType MediaType { get; }
        public List<QualityLevelInfo> QualityLevels { get; } = new List<QualityLevelInfo>();
        public List<MediaChunk> Chunks { get; } = new List<MediaChunk>();

        public StreamInfo(MediaType mediaType)
        {
            MediaType = mediaType;
        }
    }
}
