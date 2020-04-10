using System;
using System.Collections.Generic;
using System.Text;

namespace UETools.Core.Interfaces
{
    /// <summary>
    /// Interface for providing localized strings by their namespace/uid pair.
    /// </summary>
    public interface IUnrealLocalizationProvider
    {
        /// <summary>
        /// Attempts to find localized string using its <paramref name="key"/>/<paramref name="id"/> pair.
        /// </summary>
        /// <param name="key">Namespace of localized string.</param>
        /// <param name="id">UID of localized string.</param>
        /// <returns>Localized string if found; otherwise <see langword="null"/></returns>
        string? Get(string key, string id);
    }
}
