using System;
using System.Collections.Generic;
using System.Text;
using UETools.Core;
using UETools.Core.Interfaces;

namespace UETools.Objects.Structures
{
    class MaterialInputType<T> : ExpressionInput where T : unmanaged
    {
        public override FArchive Serialize(FArchive reader) 
            => base.Serialize(reader)
                   .Read(ref _useConst)
                   .ReadUnsafe(ref _const);

        public override string ToString() => (_useConst ? _const.ToString() : base.ToString())!;

        private bool _useConst;
        private T _const;
    }
}
