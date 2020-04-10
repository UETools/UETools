using System.Collections.Generic;
using System.Linq;
using UETools.Core;

namespace UETools.Objects.KismetVM.Instructions
{
    internal sealed class UnicodeStringConst : ConstToken<string>
    {
        public override EExprToken Expr => EExprToken.EX_UnicodeStringConst;
        private void ReadString16(FArchive reader, out string str)
        {
            var bytes = new List<short>();
            do
            {
                reader.Read(out short b);
                bytes.Add(b);
            } while (bytes.Last() != 0);
            str = string.Create(bytes.Count - 1, bytes, (chars, b) =>
            {
                for (int i = 0; i < chars.Length; i++)
                {
                    chars[i] = (char)b[i];
                }
            });

        }

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            ReadString16(reader, out _value);
        }
    }
}
