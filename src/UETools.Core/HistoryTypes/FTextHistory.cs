using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UETools.Core.Enums;
using UETools.Core.Interfaces;
using UETools.TypeFactory;

namespace UETools.Core.HistoryTypes
{
    internal abstract partial class FTextHistory : IUnrealSerializable
    {
        public abstract FArchive Serialize(FArchive reader);

        internal static IReadOnlyDictionary<TextHistoryType, Func<FTextHistory>> HistoryTypes { get; } = new ReadOnlyDictionary<TextHistoryType, Func<FTextHistory>>(
            Enum.GetValues(typeof(TextHistoryType))
            .Cast<TextHistoryType>()
            .ToDictionary(
                x => x,
                x => Factory.CreateInstanceFunction<FTextHistory>(
                    x.GetAttribute<LinkedTypeAttribute, TextHistoryType>()!.LinkedType
                    )
                )
            );

        public override string ToString() => string.Empty;
    }
}
