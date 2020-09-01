using System.IO;
using UETools.Core;
using UETools.Core.Interfaces;

namespace UETools.Objects.KismetVM.Instructions
{
    internal abstract class CallToken<T> : Token where T : notnull, IUnrealSerializable, new()
    {
        public T CallTo => _callTo;
        public TokenList Params { get; } = new TokenList();

        public override FArchive Serialize(FArchive archive)
        {
            base.Serialize(archive)
                .Read(ref _callTo);
            Params.ReadUntil(archive, EExprToken.EX_EndFunctionParms);
            return archive;
        }
        public override void ReadTo(TextWriter writer)
        {
            writer.Write($"{Expr} {CallTo} (");
            var i = 0;
            foreach (var item in Params.Items)
            {
                if (i++ != 0)
                    writer.Write(", ");

                item.ReadTo(writer);
            }
            writer.WriteLine(")");
        }

        private T _callTo = default!;
    }
}
