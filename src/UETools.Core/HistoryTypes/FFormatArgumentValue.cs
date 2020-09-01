using System;
using UETools.Core.Enums;
using UETools.Core.Interfaces;

namespace UETools.Core.HistoryTypes
{
    class FFormatArgumentValue : IUnrealSerializable
    {
        public object Value { get; private set; } = null!;

        public FArchive Serialize(FArchive archive)
        {
            archive.ReadUnsafe(ref _type);
            switch (_type)
            {
                case FormatArgumentType.Int:
                    {
                        long value = default!;
                        archive.Read(ref value);
                        Value = value;
                        break;
                    }
                case FormatArgumentType.UInt:
                    {
                        ulong value = default!;
                        archive.Read(ref value);
                        Value = value;
                        break;
                    }
                case FormatArgumentType.Float:
                    {
                        float value = default!;
                        archive.Read(ref value);
                        Value = value;
                        break;
                    }
                case FormatArgumentType.Double:
                    {
                        double value = default!;
                        archive.Read(ref value);
                        Value = value;
                        break;
                    }
                case FormatArgumentType.Text:
                    {
                        FText value = default!;
                        archive.Read(ref value);
                        Value = value;
                        break;
                    }
                case FormatArgumentType.Gender:
                    {
                        TextGender value = default!;
                        archive.ReadUnsafe(ref value);
                        Value = value;
                        break;
                    }
                default:
                    throw new NotImplementedException();
            }
            return archive;
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