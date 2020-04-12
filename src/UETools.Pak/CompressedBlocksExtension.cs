using System;
using System.Collections.Generic;

namespace UETools.Pak
{
    static class CompressedBlocksExtension
    {
        public static void OffsetBy(this List<PakCompressedBlock> blocks, int offset)
        {
            if (blocks is null)
                throw new ArgumentNullException(nameof(blocks));

            for (var i = 0; i < blocks.Count; i++)
                blocks[i] = blocks[i].OffsetBy(offset);
        }
    }
}
