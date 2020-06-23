//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System;

namespace Mp4Explorer
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class BoxViewTypeAttribute : Attribute
    {
        public Type BoxType { get; }

        public BoxViewTypeAttribute(Type boxType)
        {
            BoxType = boxType;
        }
    }
}
