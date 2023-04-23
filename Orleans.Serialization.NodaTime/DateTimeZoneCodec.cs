using System;
using System.Buffers;
using System.Diagnostics;
using System.Text;
using NodaTime;
using NodaTime.TimeZones;
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
        writer.WriteFieldHeader(fieldIdDelta, expectedType, typeof(DateTimeZone), WireType.LengthPrefixed);
        var bytes = Encoding.UTF8.GetBytes(value.Id);
        writer.WriteVarUInt32((uint)(bytes.Length + 1));
        writer.WriteByte((byte)(value is not BclDateTimeZone ? 1 : 2));
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
        field.EnsureWireType(WireType.LengthPrefixed);
        var length = reader.ReadVarUInt32();
        var buffer = reader.ReadBytes(length);
        var id = Encoding.UTF8.GetString(buffer.AsSpan(1));
        return buffer[0] switch
        {
            1 => DateTimeZoneProviders.Tzdb[id],
            2 => DateTimeZoneProviders.Bcl[id],
            _ => throw new UnreachableException(
                "Only 1 and 2 are valid values to indicate DateTimeZoneProvider.")
        };
    }

    public bool IsSupportedType(Type type) =>
        typeof(DateTimeZone).IsAssignableFrom(type)
        && typeof(DateTimeZone).Assembly.FullName == type.Assembly.FullName;

    private static void ThrowInvalidReference(uint reference) => throw new ReferenceNotFoundException(typeof(DateTimeZone), reference);
}
