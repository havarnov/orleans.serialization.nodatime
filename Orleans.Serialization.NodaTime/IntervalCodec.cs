using System;
using System.Buffers;
using System.Diagnostics;
using NodaTime;
using Orleans.Serialization.Buffers;
using Orleans.Serialization.Codecs;
using Orleans.Serialization.WireProtocol;

namespace Orleans.Serialization.NodaTime;

/// <summary>
/// Serializer for <see cref="Interval"/>.
/// </summary>
[RegisterSerializer]
public class IntervalCodec : IFieldCodec<Interval>
{
    private readonly InstantCodec _instantCodec = new();

    public void WriteField<TBufferWriter>(
        ref Writer<TBufferWriter> writer,
        uint fieldIdDelta,
        Type expectedType,
        Interval value)
        where TBufferWriter : IBufferWriter<byte>
    {
        ReferenceCodec.MarkValueField(writer.Session);
        writer.WriteFieldHeader(fieldIdDelta, expectedType, typeof(Interval), WireType.TagDelimited);
        _instantCodec.WriteField(ref writer, 0, typeof(Instant), value.Start);
        _instantCodec.WriteField(ref writer, 1, typeof(Instant), value.End);
        writer.WriteEndObject();

    }

    public Interval ReadValue<TInput>(
        ref Reader<TInput> reader,
        Field field)
    {
        ReferenceCodec.MarkValueField(reader.Session);

        field.EnsureWireTypeTagDelimited();

        var startField = reader.ReadFieldHeader();
        var start = _instantCodec.ReadValue(ref reader, startField);

        var endField = reader.ReadFieldHeader();
        var end = _instantCodec.ReadValue(ref reader, endField);

        var endObjectField = reader.ReadFieldHeader();
        Debug.Assert(endObjectField.IsEndBaseOrEndObject);

        return new Interval(start, end);
    }
}
