using System;
using UnrealTools.Core;
using UnrealTools.Objects.Interfaces;

namespace UnrealTools.Objects.Structures
{
    public struct Rotator : IUnrealStruct, IEquatable<Rotator>
    {
        public void Deserialize(FArchive reader)
        {
            reader.Read(out _pitch);
            reader.Read(out _roll);
            reader.Read(out _yaw);
        }
        public override string ToString() => $"{{ Pitch: {_pitch}, Roll: {_roll}, Yaw: {_yaw} }}";

        public override bool Equals(object? obj) => obj switch
        {
            Rotator rot => Equals(rot),
            _ => false
        };
        public bool Equals(Rotator other) => other._pitch == _pitch && other._yaw == _yaw && other._roll == _roll;
        public override int GetHashCode() => HashCode.Combine(_pitch, _roll, _yaw);
        public static bool operator ==(Rotator left, Rotator right) => left.Equals(right);
        public static bool operator !=(Rotator left, Rotator right) => !(left == right);

        private float _pitch;
        private float _roll;
        private float _yaw;
    }
}
