//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using Prism.Commands;

namespace Mp4Explorer
{
    public class GlobalCommandsProxy
    {
        public virtual CompositeCommand OpenCommand => GlobalCommands.OpenCommand;
    }
}
