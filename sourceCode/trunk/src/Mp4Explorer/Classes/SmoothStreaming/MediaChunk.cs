//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

namespace Mp4Explorer.SmoothStreaming
{
    public sealed class MediaChunk
    {
        public int ChunkId { get; }
        public ulong Duration { get; }

        public MediaChunk(int chunkId, ulong duration)
        {
            ChunkId = chunkId;
            Duration = duration;
        }
    }
}
