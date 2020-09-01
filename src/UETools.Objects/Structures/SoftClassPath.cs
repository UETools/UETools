using UETools.Core;
using UETools.Core.Enums;
using UETools.Objects.Interfaces;

namespace UETools.Objects.Structures
{
    struct SoftClassPath : IUnrealStruct
    {
        public FArchive Serialize(FArchive archive)
        {
            if (archive.Version < UE4Version.VER_UE4_ADDED_SOFT_OBJECT_PATH)
            {
                archive.Read(ref _path);
                _value = _path;

                // TODO: Convert path to FName value

                //if (Ar.UE4Ver() < VER_UE4_KEEP_ONLY_PACKAGE_NAMES_IN_STRING_ASSET_REFERENCES_MAP)
                //{
                //    Path = FPackageName::GetNormalizedObjectPath(Path);
                //}

                //SetPath(MoveTemp(Path));
            }
            else
            {
                archive.Read(ref _value)
                       .Read(ref _subPathString);
            }
            return archive;
        }

        public override string ToString() => _value.ToString();

        FString _path;
        FString _subPathString;
        FName _value;
    }
}
