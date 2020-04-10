using System;
using System.Diagnostics;
using UETools.Core;
using UETools.Core.Interfaces;

namespace UETools.Pak
{
    [DebuggerDisplay("{(int)_compressionStart}..{(int)_compressionEnd}")]
    public struct PakCompressedBlock : IUnrealDeserializable, IEquatable<PakCompressedBlock>
    {
        public long Start => _compressionStart;
        public void Deserialize(FArchive reader)
        {
            reader.Read(out _compressionStart);
            reader.Read(out _compressionEnd);
        }

        public readonly PakCompressedBlock AbsoluteTo(long offset)
        {
            var b = new PakCompressedBlock();
            b._compressionStart = _compressionStart - offset;
            b._compressionEnd = _compressionEnd - offset;
            return b;
        }

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
