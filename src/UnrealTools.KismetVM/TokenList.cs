using System.Collections.Generic;
using UnrealTools.Core;

namespace UnrealTools.KismetVM
{
    public class TokenList
    {
        public List<Token> Items { get; } = new List<Token>();

        public void ReadUntil(FArchive reader, EExprToken token)
        {
            Token tok;
            do
            {
                tok = TokenFactory.Read(reader);
                Items.Add(tok);
            } while (tok.Expr != token);
        }

        public void ReadCount(FArchive reader, int count)
        {
            for (int i = 0; i < count; i++)
            {
                if (TokenFactory.Read(reader) is Token tok)
                {
                    Items.Add(tok);
                }
            }
        }
    }
}
