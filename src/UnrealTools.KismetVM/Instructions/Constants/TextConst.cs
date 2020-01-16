using UnrealTools.Core;
using UnrealTools.KismetVM.Enums;

namespace UnrealTools.KismetVM.Instructions
{
    internal sealed partial class TextConst : ConstToken<FText>
    {
        public override EExprToken Expr => EExprToken.EX_TextConst;

        internal EBlueprintTextLiteralType TextLiteralType { get => _textLiteralType; set => _textLiteralType = value; }

        public override void Deserialize(FArchive reader)
        {
            base.Deserialize(reader);
            reader.ReadUnsafe(out _textLiteralType);
            switch (TextLiteralType)
            {
                case EBlueprintTextLiteralType.Empty:
                    _value = FText.GetEmpty();
                    break;
                case EBlueprintTextLiteralType.LocalizedText:
                    break;
                case EBlueprintTextLiteralType.InvariantText:
                    break;
                case EBlueprintTextLiteralType.LiteralString:
                    break;
                case EBlueprintTextLiteralType.StringTableEntry:
                    break;
                default:
                    break;
            }
            // TODO: Implement
            //reader.Read(out _value);
        }

        private EBlueprintTextLiteralType _textLiteralType;
    }
}
