using System.Collections.Generic;
using System.Linq;
using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class StringConst : ConstToken<string>
    {
        public override EExprToken Expr => EExprToken.EX_StringConst;
        private void ReadString8(FArchive reader, out string str)
        {
            var bytes = new List<byte>();
            do
            {
                bytes.Add(reader.Read<byte>());
            } while (bytes.Last() != 0);
            str = StringHelper.Create(bytes.Count - 1, bytes, (chars, b) =>
            {
                for (var i = 0; i < chars.Length; i++)
                {
                    chars[i] = (char)b[i];
                }
            });
        }

        public override FArchive Serialize(FArchive archive)
        {
            base.Serialize(archive);
            ReadString8(archive, out _value);
            return archive;
        }
    }
}
