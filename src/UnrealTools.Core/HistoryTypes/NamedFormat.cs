namespace UnrealTools.Core.HistoryTypes
{
    internal partial class FTextHistory
    {
        internal sealed class NamedFormat : FTextHistory
        {
            public override void Deserialize(FArchive reader)
            {
                base.Deserialize(reader);

                reader.Read(out _format);
                reader.Read(out _argument);
            }

            public override string ToString()
            {
                if (_format is null || _argument is null)
                    NotDeserializedException.Throw();

                return _argument.Replace(_format.ToString());
            }

            private FText _format = null!;
            private FFormatArgumentData _argument = null!;
        }
    }
}
