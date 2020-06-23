//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Collections.Generic;

namespace Mp4Explorer.SmoothStreaming.Smil
{
    public interface ISmilStream
    {
        string Src { get; }
        string SystemBitrate { get; }
        IEnumerable<SmilParam> Param { get; }
    }
}
