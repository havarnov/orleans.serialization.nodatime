using System;
using System.Buffers;
using System.Diagnostics;
using NodaTime;
using Orleans.Serialization.Buffers;
using Orleans.Serialization.Codecs;
using Orleans.Serialization.WireProtocol;

namespace Orleans.Serialization.NodaTime;

[RegisterSerializer]
public class OffsetDateTimeCodec : IFieldCodec<OffsetDateTime>
{
    private readonly LocalDateTimeCodec _localDateTimeCodec = new();
    private readonly OffsetCodec _offsetCodec = new();

    public void WriteField<TBufferWriter>(
        ref Writer<TBufferWriter> writer,
        uint fieldIdDelta,
        Type expectedType,
        OffsetDateTime value)
        where TBufferWriter : IBufferWriter<byte>
    {
        ReferenceCodec.MarkValueField(writer.Session);
        writer.WriteFieldHeader(fieldIdDelta, expectedType, typeof(OffsetDateTime), WireType.TagDelimited);
        _localDateTimeCodec.WriteField(ref writer, 0, typeof(LocalDateTime), value.LocalDateTime);
        _offsetCodec.WriteField(ref writer, 1, typeof(Offset), value.Offset);
        writer.WriteEndObject();
    }

    public OffsetDateTime ReadValue<TInput>(
        ref Reader<TInput> reader,
        Field field)
    {
        ReferenceCodec.MarkValueField(reader.Session);

        field.EnsureWireTypeTagDelimited();

        var localDateTimeField = reader.ReadFieldHeader();
        var localDateTime = _localDateTimeCodec.ReadValue(ref reader, localDateTimeField);

        var offsetField = reader.ReadFieldHeader();
        var offset = _offsetCodec.ReadValue(ref reader, offsetField);

        var end = reader.ReadFieldHeader();
        Debug.Assert(end.IsEndBaseOrEndObject);

        return localDateTime.WithOffset(offset);
    }
}
