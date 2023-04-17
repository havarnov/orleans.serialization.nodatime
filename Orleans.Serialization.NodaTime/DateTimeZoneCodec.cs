using System;
using System.Buffers;
using System.Text;
using NodaTime;
using Orleans.Serialization.Buffers;
using Orleans.Serialization.Codecs;
using Orleans.Serialization.Serializers;
using Orleans.Serialization.WireProtocol;

namespace Orleans.Serialization.NodaTime;

/// <summary>
/// Serializer for <see cref="DateTimeZone"/>.
/// </summary>
[RegisterSerializer]
public class DateTimeZoneCodec: IFieldCodec<DateTimeZone?>, IGeneralizedCodec
{
    public void WriteField<TBufferWriter>(
        ref Writer<TBufferWriter> writer,
        uint fieldIdDelta,
        Type expectedType,
        DateTimeZone? value)
        where TBufferWriter : IBufferWriter<byte>
    {
        // If the value is null, write it as the null reference.
        if (value is null)
        {
            ReferenceCodec.WriteNullReference(ref writer, fieldIdDelta);
            return;
        }

        ReferenceCodec.MarkValueField(writer.Session);
        writer.WriteFieldHeader(fieldIdDelta, expectedType, typeof(DateTimeZone), WireType.Fixed32);
        var bytes = Encoding.UTF8.GetBytes(value.Id);
        writer.WriteInt32(bytes.Length);
        writer.Write(bytes);
    }

    public DateTimeZone? ReadValue<TInput>(ref Reader<TInput> reader, Field field)
    {
        // This will only be true if the value is null.
        if (field.WireType == WireType.Reference)
        {
            ReferenceCodec.MarkValueField(reader.Session);
            var reference = reader.ReadVarUInt32();
            if (reference != 0)
            {
                ThrowInvalidReference(reference);
            }

            return null;
        }

        ReferenceCodec.MarkValueField(reader.Session);
        field.EnsureWireType(WireType.Fixed32);
        var length = reader.ReadInt32();
        var buffer = reader.ReadBytes((uint)length);
        var id = Encoding.UTF8.GetString(buffer);
        return DateTimeZoneProviders.Tzdb.GetZoneOrNull(id)
               ?? DateTimeZoneProviders.Bcl[id];
    }

    public bool IsSupportedType(Type type) => typeof(DateTimeZone).IsAssignableFrom(type);

    private static void ThrowInvalidReference(uint reference) => throw new ReferenceNotFoundException(typeof(DateTimeZone), reference);
}
