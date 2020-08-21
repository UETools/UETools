using System.IO;
using UETools.Core;
using UETools.Objects.Package;

namespace UETools.Objects.KismetVM.Instructions
{
    internal abstract class VariableToken : Token
    {
        //public ResolvedObjectReference<TaggedObject> Variable { get => _variable; set => _variable = value; }
        public ObjectReference Variable => _variable;

        public override FArchive Serialize(FArchive reader) 
            => base.Serialize(reader)
                   .Read(ref _variable);

        public override void ReadTo(TextWriter writer)
        {
            if (Variable?.Resource != null)
            {
                writer.Write("{0} {1} = ", GetType().Name, Variable.Resource.FullName);
            }
            //Variable.Value.Read(writer);
        }

        //private ResolvedObjectReference<TaggedObject> _variable;
        private ObjectReference _variable = null!;
    }
}
