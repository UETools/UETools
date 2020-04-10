using System.Collections.Generic;

namespace UETools.Core.Interfaces.Generic
{
    internal interface IUnrealTable<T> : IUnrealTable where T : notnull
    {
        List<T> Items { get; }
    }
}
