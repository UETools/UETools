using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
//using UnrealTools.CodeGen.Attributes;
using UETools.Core.Interfaces;

namespace UETools.Core
{
    public partial class FArchive
    {
        /// <summary>
        /// Deserializes array of elements to the <paramref name="item"/> <see cref="List{T}"/>.
        /// </summary>
        /// <param name="item">List of deserialized elements.</param>
        //[SpecializeMethod(typeof(bool), typeof(sbyte), typeof(byte), typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(Guid), typeof(decimal))]//, typeof(string))]
        public FArchive Read<T>([AllowNull] ref List<T> item)
            where T : IUnrealSerializable?, new() 
            => Read(ref item, Read<int>());
        /// <summary>
        /// Deserializes array of elements to the <paramref name="item"/> <see cref="List{T}"/>, using declared <paramref name="length"/>.
        /// </summary>
        /// <param name="item">List of deserialized elements.</param>
        /// <param name="length">Count of elements.</param>
        //[SpecializeMethod(typeof(bool), typeof(sbyte), typeof(byte), typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(Guid), typeof(decimal))]//, typeof(string))]
        public FArchive Read<T>([AllowNull] ref List<T> item, int length) where T : IUnrealSerializable?, new()
        {
            var it = new List<T>(length);
            for (var i = 0; i < length; i++)
            {
                T x = default!;
                Read(ref x);
                it.Add(x);
            }
            item = it;
            return this;
        }

        public FArchive Read([AllowNull] ref List<int> item) => Read(ref item, Read<int>());
        public FArchive Read([AllowNull] ref List<int> item, int length)
        {
            var it = new List<int>(length);
            for (var i = 0; i < length; i++)
            {
                int x = 0;
                Read(ref x);
                it.Add(x);
            }
            item = it;
            return this;
        }
        public FArchive Read([AllowNull] ref List<uint> item) => Read(ref item, Read<int>());
        public FArchive Read([AllowNull] ref List<uint> item, int length)
        {
            var it = new List<uint>(length);
            for (var i = 0; i < length; i++)
            {
                uint x = 0;
                Read(ref x);
                it.Add(x);
            }
            item = it;
            return this;
        }
        public FArchive Read([AllowNull] ref List<ushort> item) => Read(ref item, Read<int>());
        public FArchive Read([AllowNull] ref List<ushort> item, int length)
        {
            var it = new List<ushort>(length);
            for (var i = 0; i < length; i++)
            {
                ushort x = 0;
                Read(ref x);
                it.Add(x);
            }
            item = it;
            return this;
        }
        public FArchive Read([AllowNull] ref List<float> item) => Read(ref item, Read<int>());
        public FArchive Read([AllowNull] ref List<float> item, int length)
        {
            var it = new List<float>(length);
            for (var i = 0; i < length; i++)
            {
                float x = 0;
                Read(ref x);
                it.Add(x);
            }
            item = it;
            return this;
        }

        /// <summary>
        /// Deserializes array of elements to the <paramref name="item"/> <see cref="Dictionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="item">Dictionary of deserialized elements.</param>
        //[SpecializeMethod(typeof(bool), typeof(sbyte), typeof(byte), typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(Guid), typeof(decimal))]//, typeof(string))]
        public FArchive Read<TKey, TValue>([AllowNull] ref Dictionary<TKey, TValue> item)
            where TKey : notnull, IUnrealSerializable, new()
            where TValue : IUnrealSerializable?, new()
            => Read(ref item, Read<int>());

        /// <summary>
        /// Deserializes array of elements to the <paramref name="item"/> <see cref="Dictionary{TKey, TValue}"/>, using declared <paramref name="length"/>.
        /// </summary>
        /// <param name="item">Dictionary of deserialized elements.</param>
        /// <param name="length">Count of elements.</param>
        //[SpecializeMethod(typeof(bool), typeof(sbyte), typeof(byte), typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(Guid), typeof(decimal))]//, typeof(string))]
        public FArchive Read<TKey, TValue>([AllowNull] ref Dictionary<TKey, TValue> item, int length)
            where TKey : notnull, IUnrealSerializable, new()
            where TValue : IUnrealSerializable?, new()
        {
            var it = new Dictionary<TKey, TValue>(length);
            for (var i = 0; i < length; i++)
            {
                TKey x = default!; 
                TValue y = default!;
                Read(ref x);
                Read(ref y);
                it.Add(x, y);
            }
            item = it;
            return this;
        }

        public FArchive Read([AllowNull] ref Dictionary<ushort, ushort> item)
            => Read(ref item, Read<int>());
        public FArchive Read([AllowNull] ref Dictionary<ushort, ushort> item, int length)
        {
            var it = new Dictionary<ushort, ushort>(length);
            for (var i = 0; i < length; i++)
            {
                ushort x = 0, y = 0;
                Read(ref x);
                Read(ref y);
                it.Add(x, y);
            }
            item = it;
            return this;
        }
    }
}
