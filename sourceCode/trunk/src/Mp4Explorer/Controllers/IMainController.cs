//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using CMStream.Mp4;

namespace Mp4Explorer
{
    public interface IMainController
    {
        void OnBoxSelected(Mp4Box box);
        void ShowFile(Mp4File file);
        void RemoveViews();
    }
}
