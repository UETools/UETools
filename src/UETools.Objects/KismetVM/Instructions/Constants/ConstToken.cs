using System.IO;

namespace UETools.Objects.KismetVM.Instructions
{
    internal abstract class ConstToken<T> : Token
    {
        public T Value { get => _value; set => _value = value; }

        public override void ReadTo(TextWriter writer) => writer.Write(_value);

        protected T _value = default!;
    }
}
