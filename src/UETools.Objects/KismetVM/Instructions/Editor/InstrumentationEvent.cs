using System.IO;
using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed partial class InstrumentationEvent : Token
    {
        public override EExprToken Expr => EExprToken.EX_InstrumentationEvent;

        public EScriptInstrumentation EventType => _eventType;

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            reader.ReadUnsafe(out _eventType);
        }

        public override void ReadTo(TextWriter writer) => writer.WriteLine(EventType);

        private EScriptInstrumentation _eventType;
    }
}
