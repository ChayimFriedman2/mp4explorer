//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using CMStream.Mp4;

namespace Mp4Explorer
{
    public interface IMainService
    {
        IBoxView GetView(Mp4Box box);
    }
}
