using System;
using System.Buffers;
using System.Diagnostics;
using NodaTime;
using Orleans.Serialization.Buffers;
using Orleans.Serialization.Codecs;
using Orleans.Serialization.WireProtocol;

namespace Orleans.Serialization.NodaTime;

[RegisterSerializer]
public class OffsetDateCodec : IFieldCodec<OffsetDate>
{
    private readonly LocalDateCodec _localDateCodec = new();
    private readonly OffsetCodec _offsetCodec = new();

    public void WriteField<TBufferWriter>(
        ref Writer<TBufferWriter> writer,
        uint fieldIdDelta,
        Type expectedType,
        OffsetDate value)
        where TBufferWriter : IBufferWriter<byte>
    {
        ReferenceCodec.MarkValueField(writer.Session);
        writer.WriteFieldHeader(fieldIdDelta, expectedType, typeof(OffsetDate), WireType.TagDelimited);
        _localDateCodec.WriteField(ref writer, 0, typeof(LocalDate), value.Date);
        _offsetCodec.WriteField(ref writer, 1, typeof(Offset), value.Offset);
        writer.WriteEndObject();
    }

    public OffsetDate ReadValue<TInput>(
        ref Reader<TInput> reader,
        Field field)
    {
        ReferenceCodec.MarkValueField(reader.Session);

        field.EnsureWireTypeTagDelimited();

        var localDateField = reader.ReadFieldHeader();
        var localDate = _localDateCodec.ReadValue(ref reader, localDateField);

        var offsetField = reader.ReadFieldHeader();
        var offset = _offsetCodec.ReadValue(ref reader, offsetField);

        var end = reader.ReadFieldHeader();
        Debug.Assert(end.IsEndBaseOrEndObject);

        return localDate.WithOffset(offset);
    }
}
