using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public List<T> Read<T>(out List<T> item)
            where T : IUnrealDeserializable?, new() 
            => Read(out item, Read(out int _));
        /// <summary>
        /// Deserializes array of elements to the <paramref name="item"/> <see cref="List{T}"/>, using declared <paramref name="length"/>.
        /// </summary>
        /// <param name="item">List of deserialized elements.</param>
        /// <param name="length">Count of elements.</param>
        //[SpecializeMethod(typeof(bool), typeof(sbyte), typeof(byte), typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(Guid), typeof(decimal))]//, typeof(string))]
        public List<T> Read<T>(out List<T> item, int length) where T : IUnrealDeserializable?, new()
        {
            var it = new List<T>(length);
            for (var i = 0; i < length; i++)
            {
                Read(out T x);
                it.Add(x);
            }
            return item = it;
        }

        public List<int> Read(out List<int> item) => Read(out item, Read(out int _));
        public List<int> Read(out List<int> item, int length)
        {
            var it = new List<int>(length);
            for (var i = 0; i < length; i++)
            {
                Read(out int x);
                it.Add(x);
            }
            return item = it;
        }
        public List<uint> Read(out List<uint> item) => Read(out item, Read(out int _));
        public List<uint> Read(out List<uint> item, int length)
        {
            var it = new List<uint>(length);
            for (var i = 0; i < length; i++)
            {
                Read(out uint x);
                it.Add(x);
            }
            return item = it;
        }
        public List<ushort> Read(out List<ushort> item) => Read(out item, Read(out int _));
        public List<ushort> Read(out List<ushort> item, int length)
        {
            var it = new List<ushort>(length);
            for (var i = 0; i < length; i++)
            {
                Read(out ushort x);
                it.Add(x);
            }
            return item = it;
        }
        public List<float> Read(out List<float> item) => Read(out item, Read(out int _));
        public List<float> Read(out List<float> item, int length)
        {
            var it = new List<float>(length);
            for (var i = 0; i < length; i++)
            {
                Read(out float x);
                it.Add(x);
            }
            return item = it;
        }

        /// <summary>
        /// Deserializes array of elements to the <paramref name="item"/> <see cref="Dictionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="item">Dictionary of deserialized elements.</param>
        //[SpecializeMethod(typeof(bool), typeof(sbyte), typeof(byte), typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(Guid), typeof(decimal))]//, typeof(string))]
        public Dictionary<TKey, TValue> Read<TKey, TValue>(out Dictionary<TKey, TValue> item)
            where TKey : notnull, IUnrealDeserializable, new()
            where TValue : IUnrealDeserializable?, new()
            => Read(out item, Read(out int _));

        /// <summary>
        /// Deserializes array of elements to the <paramref name="item"/> <see cref="Dictionary{TKey, TValue}"/>, using declared <paramref name="length"/>.
        /// </summary>
        /// <param name="item">Dictionary of deserialized elements.</param>
        /// <param name="length">Count of elements.</param>
        //[SpecializeMethod(typeof(bool), typeof(sbyte), typeof(byte), typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(Guid), typeof(decimal))]//, typeof(string))]
        public Dictionary<TKey, TValue> Read<TKey, TValue>(out Dictionary<TKey, TValue> item, int length)
            where TKey : notnull, IUnrealDeserializable, new()
            where TValue : IUnrealDeserializable?, new()
        {
            var it = new Dictionary<TKey, TValue>(length);
            for (var i = 0; i < length; i++)
            {
                Read(out TKey x);
                Read(out TValue y);
                it.Add(x, y);
            }
            return item = it;
        }

        public Dictionary<ushort, ushort> Read(out Dictionary<ushort, ushort> item)
            => Read(out item, Read(out int _));
        public Dictionary<ushort, ushort> Read(out Dictionary<ushort, ushort> item, int length)
        {
            var it = new Dictionary<ushort, ushort>(length);
            for (var i = 0; i < length; i++)
            {
                Read(out ushort x);
                Read(out ushort y);
                it.Add(x, y);
            }
            return item = it;
        }
    }
}
