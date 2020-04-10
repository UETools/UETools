using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UETools.Core;
using UETools.TypeFactory;

namespace UETools.Objects.KismetVM
{
    public sealed class TokenFactory
    {
        public static Token Read(FArchive reader)
        {
            reader.ReadUnsafe(out EExprToken currentToken);
            if (EnumTypes.TryGetValue(currentToken, out var type))
            {
                var tok = type();
                tok.Deserialize(reader);
                return tok;
            }
            throw new NotImplementedException($"Unknown opcode {currentToken}");
        }

        public static List<Type> Types { get; } = typeof(Token).Assembly.GetTypes()
            .Where(t => t.IsSealed && t.IsSubclassOf(typeof(Token)))
            .ToList();

        public static IReadOnlyDictionary<EExprToken, Func<Token>> EnumTypes { get; } = new ReadOnlyDictionary<EExprToken, Func<Token>>(
            Enum.GetValues(typeof(EExprToken))
                .Cast<EExprToken>()
                .ToDictionary(
                    t => t,
                    v => Factory.CreateInstanceFunction<Token>(Types.FirstOrDefault(t => v.ToString().AsSpan().Slice(3) == t.Name.AsSpan()))
                ));
    }
}
