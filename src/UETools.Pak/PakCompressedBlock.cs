using System;
using System.Diagnostics;
using UETools.Core;
using UETools.Core.Interfaces;

namespace UETools.Pak
{
    [DebuggerDisplay("{(int)_compressionStart}..{(int)_compressionEnd}")]
    public struct PakCompressedBlock : IUnrealSerializable, IEquatable<PakCompressedBlock>
    {
        public PakCompressedBlock(long start, long end)
        {
            _compressionStart = start;
            _compressionEnd = end;
        }

        public long Start => _compressionStart;
        public long End => _compressionEnd;
        public FArchive Serialize(FArchive reader)
            => reader.Read(ref _compressionStart)
                     .Read(ref _compressionEnd);

        internal readonly PakCompressedBlock OffsetBy(long offset) 
            => new PakCompressedBlock(_compressionStart - offset, _compressionEnd - offset);

        private long _compressionStart;
        private long _compressionEnd;

        public readonly override bool Equals(object? obj) => obj switch
        {
            PakCompressedBlock block => Equals(block),
            _ => false
        };
        public readonly override int GetHashCode() => HashCode.Combine(_compressionStart, _compressionEnd);
        public readonly bool Equals(PakCompressedBlock other) => other._compressionStart == _compressionStart && other._compressionEnd == _compressionEnd;
        public static bool operator ==(PakCompressedBlock left, PakCompressedBlock right) => left.Equals(right);
        public static bool operator !=(PakCompressedBlock left, PakCompressedBlock right) => !(left == right);
    }
}
