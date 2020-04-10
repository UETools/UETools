using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace UETools.Core
{
    public static class EnumAttributesExtensions
    {
        public static TAttrib? GetAttribute<TAttrib, TEnum>(this TEnum value)
            where TAttrib : notnull, Attribute
            where TEnum : notnull, Enum
        {
            var enumType = typeof(TEnum);
            var fields = enumType.GetMember(value.ToString());
            return fields.FirstOrDefault()?.GetCustomAttribute<TAttrib>();
        }

        public static bool TryGetAttribute<TAttrib, TEnum>(this TEnum value, [NotNullWhen(true)] out TAttrib? attribute)
            where TAttrib : notnull, Attribute
            where TEnum : notnull, Enum
        {
            var enumType = typeof(TEnum);
            var fields = enumType.GetMember(value.ToString());
            attribute = fields.FirstOrDefault()?.GetCustomAttribute<TAttrib>();
            return attribute != null;
        }
    }
}
