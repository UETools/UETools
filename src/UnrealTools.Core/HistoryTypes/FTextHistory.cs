using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnrealTools.Core.Enums;
using UnrealTools.Core.Interfaces;
using UnrealTools.TypeFactory;

namespace UnrealTools.Core.HistoryTypes
{
    internal partial class FTextHistory : IUnrealDeserializable
    {
        public virtual void Deserialize(FArchive reader) { }

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
