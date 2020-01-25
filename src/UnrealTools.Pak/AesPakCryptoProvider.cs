using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using UnrealTools.Pak.Interfaces;

namespace UnrealTools.Pak
{
    internal sealed class AesPakCryptoProvider : IDisposable
    {
        private ICryptoTransform _decryptor;
        private ICryptoTransform _encryptor;
        public AesPakCryptoProvider(IAesKeyProvider keyProvider)
        {
            if (keyProvider is null) 
                throw new ArgumentNullException(nameof(keyProvider));

            using var _aesProvider = new AesCryptoServiceProvider();
            _aesProvider.IV = new byte[16];
            _aesProvider.Mode = CipherMode.ECB;
            _aesProvider.Padding = PaddingMode.Zeros;
            _aesProvider.Key = Convert.FromBase64String(keyProvider.DecryptionKey);
            _decryptor = _aesProvider.CreateDecryptor();
            _encryptor = _aesProvider.CreateEncryptor();
        }

        public void Decrypt(Memory<byte> buffer)
        {
            if (MemoryMarshal.TryGetArray<byte>(buffer, out var segment))
            {
                Decrypt(segment.Array!, segment.Offset, segment.Count);
            }
            else
            {
                var pool = ArrayPool<byte>.Shared.Rent(buffer.Length);
                Decrypt(pool, 0, buffer.Length);
                pool.CopyTo(buffer);
                ArrayPool<byte>.Shared.Return(pool);
            }
        }
        public void Encrypt(Memory<byte> buffer)
        {
            if (MemoryMarshal.TryGetArray<byte>(buffer, out var segment))
            {
                Encrypt(segment.Array!, segment.Offset, segment.Count);
            }
            else
            {
                var pool = ArrayPool<byte>.Shared.Rent(buffer.Length);
                Encrypt(pool, 0, buffer.Length);
                pool.CopyTo(buffer);
                ArrayPool<byte>.Shared.Return(pool);
            }
        }
        public void Decrypt(byte[] array, int offset, int count)
        {
            // Aes implementation doesn't need TransformFinalBlock, where it actually allocates array to return
            var blockSize = _decryptor.InputBlockSize;
            while (count > 0)
            {
                var read = _decryptor.TransformBlock(array, offset, Math.Min(blockSize, count), array, offset);
                count -= read;
                offset += read;
            }
        }
        public void Encrypt(byte[] array, int offset, int count)
        {
            // Aes implementation doesn't need TransformFinalBlock, where it actually allocates array to return
            var blockSize = _encryptor.OutputBlockSize;
            while (count > 0)
            {
                var read = _encryptor.TransformBlock(array, offset, Math.Min(blockSize, count), array, offset);
                count -= read;
                offset += read;
            }
        }

        public void Dispose()
        {
            _decryptor.Dispose();
            _encryptor.Dispose();
        }
    }
}
