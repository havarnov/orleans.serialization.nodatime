using System;
using System.Buffers;
using System.Diagnostics;
using NodaTime;
using Orleans.Serialization.Buffers;
using Orleans.Serialization.Codecs;
using Orleans.Serialization.WireProtocol;

namespace Orleans.Serialization.NodaTime;

/// <summary>
/// Codec for <see cref="LocalDateTime"/>.
/// </summary>
[RegisterSerializer]
public class LocalDateTimeCodec : IFieldCodec<LocalDateTime>
{
    private readonly LocalDateCodec _localDateCodec = new();
    private readonly LocalTimeCodec _localTimeCodec = new();
    public void WriteField<TBufferWriter>(
        ref Writer<TBufferWriter> writer,
        uint fieldIdDelta,
        Type expectedType,
        LocalDateTime value)
        where TBufferWriter : IBufferWriter<byte>
    {
        ReferenceCodec.MarkValueField(writer.Session);
        writer.WriteFieldHeader(fieldIdDelta, expectedType, typeof(LocalDateTime), WireType.TagDelimited);
        _localDateCodec.WriteField(ref writer, 0, typeof(LocalDate), value.Date);
        _localTimeCodec.WriteField(ref writer, 1, typeof(LocalTime), value.TimeOfDay);
        writer.WriteEndObject();

    }

    public LocalDateTime ReadValue<TInput>(ref Reader<TInput> reader, Field field)
    {
        ReferenceCodec.MarkValueField(reader.Session);

        field.EnsureWireTypeTagDelimited();

        var localDateField = reader.ReadFieldHeader();
        var localDate = _localDateCodec.ReadValue(ref reader, localDateField);

        var localTimeField = reader.ReadFieldHeader();
        var localTime = _localTimeCodec.ReadValue(ref reader, localTimeField);

        var end = reader.ReadFieldHeader();
        Debug.Assert(end.IsEndBaseOrEndObject);

        return localDate.At(localTime);
    }
}
