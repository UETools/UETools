using System.CodeDom.Compiler;
using UETools.Core;
using UETools.Objects.Classes;
using UETools.Objects.Interfaces;
using UETools.Objects.Package;

namespace UETools.Objects.Property
{
    internal sealed class ObjectProperty : UProperty<ObjectReference>
    {
        public override void Deserialize(FArchive reader, PropertyTag tag) => reader.Read(out _value);

        public override void ReadTo(IndentedTextWriter writer)
        {
            if(_value is null)
                NotDeserializedException.Throw();

            if(_value.Resource is ObjectExport exp && !exp.WasPrinted && (exp.IsDefaultObject || exp.IsArchetypeObject))
            {
                exp.WasPrinted = true;
                writer.Indent++;
                ReadObject(writer, exp.Read(null));
                writer.Indent--;
            }
            else 
                writer.WriteLine(_value.ToString());
        }
    }
}