using System;
using System.Buffers;
using System.Diagnostics;
using NodaTime;
using Orleans.Serialization.Buffers;
using Orleans.Serialization.Codecs;
using Orleans.Serialization.WireProtocol;

namespace Orleans.Serialization.NodaTime;

[RegisterSerializer]
public class OffsetTimeCodec : IFieldCodec<OffsetTime>
{
    private readonly LocalTimeCodec _localTimeCodec = new();
    private readonly OffsetCodec _offsetCodec = new();

    public void WriteField<TBufferWriter>(
        ref Writer<TBufferWriter> writer,
        uint fieldIdDelta,
        Type expectedType,
        OffsetTime value)
        where TBufferWriter : IBufferWriter<byte>
    {
        ReferenceCodec.MarkValueField(writer.Session);
        writer.WriteFieldHeader(fieldIdDelta, expectedType, typeof(OffsetTime), WireType.TagDelimited);
        _localTimeCodec.WriteField(ref writer, 0, typeof(LocalTime), value.TimeOfDay);
        _offsetCodec.WriteField(ref writer, 1, typeof(Offset), value.Offset);
        writer.WriteEndObject();
    }

    public OffsetTime ReadValue<TInput>(
        ref Reader<TInput> reader,
        Field field)
    {
        ReferenceCodec.MarkValueField(reader.Session);

        field.EnsureWireTypeTagDelimited();

        var localTimeField = reader.ReadFieldHeader();
        var localTime = _localTimeCodec.ReadValue(ref reader, localTimeField);

        var offsetField = reader.ReadFieldHeader();
        var offset = _offsetCodec.ReadValue(ref reader, offsetField);

        var end = reader.ReadFieldHeader();
        Debug.Assert(end.IsEndBaseOrEndObject);

        return localTime.WithOffset(offset);
    }
}
