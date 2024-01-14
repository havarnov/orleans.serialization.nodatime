using System;
using System.Buffers;
using System.Diagnostics;
using NodaTime;
using Orleans.Serialization.Buffers;
using Orleans.Serialization.Codecs;
using Orleans.Serialization.WireProtocol;

namespace Orleans.Serialization.NodaTime;

[RegisterSerializer]
public class ZonedDateTimeCodec : IFieldCodec<ZonedDateTime>
{
    private readonly InstantCodec _instantCodec = new();
    private readonly DateTimeZoneCodec _dateTimeZoneCodec = new();

    public void WriteField<TBufferWriter>(
        ref Writer<TBufferWriter> writer,
        uint fieldIdDelta,
        Type expectedType,
        ZonedDateTime value)
        where TBufferWriter : IBufferWriter<byte>
    {
        ReferenceCodec.MarkValueField(writer.Session);
        writer.WriteFieldHeader(fieldIdDelta, expectedType, typeof(ZonedDateTime), WireType.TagDelimited);
        _instantCodec.WriteField(ref writer, 0, typeof(Instant), value.ToInstant());
        _dateTimeZoneCodec.WriteField(ref writer, 1, typeof(DateTimeZone), value.Zone);
        writer.WriteEndObject();
    }

    public ZonedDateTime ReadValue<TInput>(
        ref Reader<TInput> reader,
        Field field)
    {
        ReferenceCodec.MarkValueField(reader.Session);

        field.EnsureWireTypeTagDelimited();

        var instantField = reader.ReadFieldHeader();
        var instant = _instantCodec.ReadValue(ref reader, instantField);

        var zoneField = reader.ReadFieldHeader();
        var zone = _dateTimeZoneCodec.ReadValue(ref reader, zoneField);
        Debug.Assert(zone is not null);

        var end = reader.ReadFieldHeader();
        Debug.Assert(end.IsEndBaseOrEndObject);

        return instant.InZone(zone);
    }
}
