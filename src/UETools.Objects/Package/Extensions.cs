using UETools.Core;

namespace UETools.Objects.Package
{
    public static class Extensions
    {
        /// <summary>
        /// Resolves <paramref name="index"/> to either import or export 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="index">Index of resource. Negative value notates imports, positive exports.</param>
        /// <returns>Resolved resource, or <see langword="null"/>.</returns>
        public static ObjectResource? ImpExp(this FArchive reader, PackageIndex index) => index.Resolve(reader);
    }
}
