using System;
using UETools.Core.Enums;
using UETools.Core.Interfaces;

namespace UETools.Core.HistoryTypes
{
    class FFormatArgumentValue : IUnrealSerializable
    {
        public object Value { get; private set; } = null!;

        public FArchive Serialize(FArchive reader)
        {
            reader.ReadUnsafe(ref _type);
            switch (_type)
            {
                case FormatArgumentType.Int:
                    {
                        long value = default!;
                        reader.Read(ref value);
                        Value = value;
                        break;
                    }
                case FormatArgumentType.UInt:
                    {
                        ulong value = default!;
                        reader.Read(ref value);
                        Value = value;
                        break;
                    }
                case FormatArgumentType.Float:
                    {
                        float value = default!;
                        reader.Read(ref value);
                        Value = value;
                        break;
                    }
                case FormatArgumentType.Double:
                    {
                        double value = default!;
                        reader.Read(ref value);
                        Value = value;
                        break;
                    }
                case FormatArgumentType.Text:
                    {
                        FText value = default!;
                        reader.Read(ref value);
                        Value = value;
                        break;
                    }
                case FormatArgumentType.Gender:
                    {
                        TextGender value = default!;
                        reader.ReadUnsafe(ref value);
                        Value = value;
                        break;
                    }
                default:
                    throw new NotImplementedException();
            }
            return reader;
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