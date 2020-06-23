//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

namespace Mp4Explorer.SmoothStreaming
{
    public sealed class QualityLevelInfo
    {
        public ulong Bitrate { get; }
        public int TrackId { get; set; }
        public string Filename { get; set; }

        public QualityLevelInfo(ulong bitrate)
        {
            Bitrate = bitrate;
            TrackId = -1;
        }
    }
}
