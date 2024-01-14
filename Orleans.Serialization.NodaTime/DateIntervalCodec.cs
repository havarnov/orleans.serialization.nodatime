using System;
using System.Buffers;
using System.Diagnostics;
using NodaTime;
using Orleans.Serialization.Buffers;
using Orleans.Serialization.Codecs;
using Orleans.Serialization.WireProtocol;

namespace Orleans.Serialization.NodaTime;

/// <summary>
/// Serializer for <see cref="DateInterval"/>.
/// </summary>
[RegisterSerializer]
public class DateIntervalCodec : IFieldCodec<DateInterval?>
{
    private readonly LocalDateCodec _localDateCodec = new();

    public void WriteField<TBufferWriter>(
        ref Writer<TBufferWriter> writer,
        uint fieldIdDelta,
        Type expectedType,
        DateInterval? value)
        where TBufferWriter : IBufferWriter<byte>
    {
        if (ReferenceCodec.TryWriteReferenceField(ref writer, fieldIdDelta, expectedType, value))
        {
            return;
        }

        // 'value' can't be null since ReferenceCodec.TryWriteReferenceField would always be able to write a 'null' field.
        Debug.Assert(value is not null);

        writer.WriteFieldHeader(fieldIdDelta, expectedType, typeof(DateInterval), WireType.TagDelimited);
        _localDateCodec.WriteField(ref writer, 0, typeof(LocalDate), value.Start);
        _localDateCodec.WriteField(ref writer, 1, typeof(LocalDate), value.End);
        writer.WriteEndObject();
    }

    public DateInterval? ReadValue<TInput>(
        ref Reader<TInput> reader,
        Field field)
    {
        if (field.WireType == WireType.Reference)
        {
            return ReferenceCodec.ReadReference<DateInterval?, TInput>(ref reader, field);
        }

        field.EnsureWireTypeTagDelimited();

        var startDateField = reader.ReadFieldHeader();
        var startDate = _localDateCodec.ReadValue(ref reader, startDateField);

        var endDateField = reader.ReadFieldHeader();
        var endDate = _localDateCodec.ReadValue(ref reader, endDateField);

        var end = reader.ReadFieldHeader();
        Debug.Assert(end.IsEndBaseOrEndObject);

        var value = new DateInterval(startDate, endDate);
        ReferenceCodec.RecordObject(reader.Session, value);
        return value;
    }
}
