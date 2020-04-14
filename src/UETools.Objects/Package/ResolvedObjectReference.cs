using System.Diagnostics;
using UETools.Core;
using UETools.Objects.Classes;

namespace UETools.Objects.Package
{
    [DebuggerDisplay("Ref: {Value}")]
    public sealed class ResolvedObjectReference<T> : ObjectReference where T : TaggedObject
    {
        public T? Value => Resource?.Read(null) as T;
    }
}
