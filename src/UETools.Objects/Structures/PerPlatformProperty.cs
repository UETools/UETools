using System;
using System.Collections.Generic;
using System.Text;
using UETools.Core;
using UETools.Objects.Interfaces;

namespace UETools.Objects.Structures
{
    // TODO: Consider IProperty being IUnrealSerializable with FArchive wrapper
    abstract class PerPlatformProperty<T> : IUnrealStruct where T : struct
    {
        public virtual FArchive Serialize(FArchive archive) => archive.Read(ref _isCooked);

        public override string ToString() => _value.ToString()!;


        bool _isCooked;
        protected T _value;
    }
    sealed class PerPlatformInt : PerPlatformProperty<int>
    {
        public override FArchive Serialize(FArchive archive) => base.Serialize(archive).Read(ref _value);
    }
    sealed class FreezablePerPlatformInt : PerPlatformProperty<int>
    {
        public override FArchive Serialize(FArchive archive) => base.Serialize(archive).Read(ref _value);
    }
    sealed class PerPlatformFloat : PerPlatformProperty<float>
    {
        public override FArchive Serialize(FArchive archive) => base.Serialize(archive).Read(ref _value);
    }
    sealed class FFreezablePerPlatformFloat : PerPlatformProperty<float>
    {
        public override FArchive Serialize(FArchive archive) => base.Serialize(archive).Read(ref _value);
    }
    sealed class PerPlatformBool : PerPlatformProperty<bool>
    {
        public override FArchive Serialize(FArchive archive) => base.Serialize(archive).Read(ref _value);
    }
}
