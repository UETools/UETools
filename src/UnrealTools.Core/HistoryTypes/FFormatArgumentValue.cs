using System;
using UnrealTools.Core.Enums;
using UnrealTools.Core.Interfaces;

namespace UnrealTools.Core.HistoryTypes
{
    class FFormatArgumentValue : IUnrealDeserializable
    {
        public object Value { get; private set; } = null!;

        public void Deserialize(FArchive reader)
        {
            reader.ReadUnsafe(out _type);
            switch (_type)
            {
                case FormatArgumentType.Int:
                    reader.Read(out long ivalue);
                    Value = ivalue;
                    break;
                case FormatArgumentType.UInt:
                    reader.Read(out ulong uivalue);
                    Value = uivalue;
                    break;
                case FormatArgumentType.Float:
                    reader.Read(out float fvalue);
                    Value = fvalue;
                    break;
                case FormatArgumentType.Double:
                    reader.Read(out double dvalue);
                    Value = dvalue;
                    break;
                case FormatArgumentType.Text:
                    reader.Read(out FText tvalue);
                    Value = tvalue;
                    break;
                case FormatArgumentType.Gender:
                    reader.ReadUnsafe(out TextGender evalue);
                    Value = evalue;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public override string ToString()
        { 
            if (Value is null) 
                NotDeserializedException.Throw(); 
            return Value.ToString()!; 
        }

        private FormatArgumentType _type;
    }
}