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
        if (ReferenceCodec.TryWriteReferenceField(ref writer, fieldIdDelta, expectedType, value))
        {
            return;
        }

        // 'value' can't be null since ReferenceCodec.TryWriteReferenceField would always be able to write a 'null' field.
        Debug.Assert(value is not null);

        writer.WriteFieldHeader(fieldIdDelta, expectedType, typeof(DateTimeZone), WireType.LengthPrefixed);
        var bytes = Encoding.UTF8.GetBytes(value.Id);
        writer.WriteVarUInt32((uint)(bytes.Length + 1));
        writer.WriteByte((byte)(value is not BclDateTimeZone ? 1 : 2));
        writer.Write(bytes);
    }

    public DateTimeZone? ReadValue<TInput>(ref Reader<TInput> reader, Field field)
    {
        if (field.WireType == WireType.Reference)
        {
            return ReferenceCodec.ReadReference<DateTimeZone?, TInput>(ref reader, field);
        }

        field.EnsureWireType(WireType.LengthPrefixed);
        var length = reader.ReadVarUInt32();
        var buffer = reader.ReadBytes(length);
        var id = Encoding.UTF8.GetString(buffer.AsSpan(1));
        var value = buffer[0] switch
        {
            1 => DateTimeZoneProviders.Tzdb[id],
            2 => DateTimeZoneProviders.Bcl[id],
            _ => throw new UnreachableException(
                "Only 1 and 2 are valid values to indicate DateTimeZoneProvider.")
        };

        ReferenceCodec.RecordObject(reader.Session, value);
        return value;
    }

    public bool IsSupportedType(Type? type) =>
        type is not null
        && typeof(DateTimeZone).IsAssignableFrom(type)
        && typeof(DateTimeZone).Assembly.FullName == type.Assembly.FullName;
}
