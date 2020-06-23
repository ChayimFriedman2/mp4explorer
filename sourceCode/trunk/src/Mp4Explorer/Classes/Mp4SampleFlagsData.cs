//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System;

namespace Mp4Explorer
{
    public struct Mp4SampleFlagsData : IEquatable<Mp4SampleFlagsData>
    {
        private readonly uint _Flags;

        public uint Reserverd => (_Flags >> 26) & 0x3F;
        public bool SampleDependsOn => ((_Flags >> 24) & 0x03) != 0;
        public bool SampleIsDependOn => ((_Flags >> 22) & 0x03) != 0;
        public bool SampleHasRedudancy => ((_Flags >> 20) & 0x03) != 0;
        public byte SamplePaddingValue => (byte)((_Flags >> 17) & 0x07);
        /// <summary>
        /// When 1 signals a non-key or non-sync sample.
        /// </summary>
        public bool SampleIsDifferenceSample => ((_Flags >> 16) & 0x01) != 0;
        public uint SampleDegradationPriority => (byte)((_Flags >> 17) & 0xFFFF);

        public Mp4SampleFlagsData(uint flags)
        {
            _Flags = flags;
        }

        public override bool Equals(object obj) => obj is Mp4SampleFlagsData other && Equals(other);

        public override int GetHashCode() => _Flags.GetHashCode();

        public bool Equals(Mp4SampleFlagsData other) => _Flags == other._Flags;

        public static bool operator ==(Mp4SampleFlagsData left, Mp4SampleFlagsData right) => left.Equals(right);
        public static bool operator !=(Mp4SampleFlagsData left, Mp4SampleFlagsData right) => !(left == right);
    }
}
