using System;
using System.Collections.Generic;
using System.Text;
using UnrealTools.Core;
using UnrealTools.Core.Enums;
using UnrealTools.Objects.Interfaces;

namespace UnrealTools.Objects.Structures
{
    struct SoftObjectPath : IUnrealStruct
    {
        public void Deserialize(FArchive reader)
        {
            if (reader.Version < UE4Version.VER_UE4_ADDED_SOFT_OBJECT_PATH)
            {
                reader.Read(out _path);
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
                reader.Read(out _value);
                reader.Read(out _subPathString);
            }
        }
        public override string ToString() => _value.ToString();

        FString _path;
        FString _subPathString;
        FName _value;
    }
}
