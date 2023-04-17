using System;
using System.Buffers;
using NodaTime;
using Orleans.Serialization.Buffers;
using Orleans.Serialization.Codecs;
using Orleans.Serialization.WireProtocol;

namespace Orleans.Serialization.NodaTime;

/// <summary>
/// Serializer for <see cref="LocalTime"/>.
/// </summary>
[RegisterSerializer]
public class LocalTimeCodec:  IFieldCodec<LocalTime>
{
    public void WriteField<TBufferWriter>(
        ref Writer<TBufferWriter> writer,
        uint fieldIdDelta,
        Type expectedType,
        LocalTime value)
        where TBufferWriter : IBufferWriter<byte>
    {
        ReferenceCodec.MarkValueField(writer.Session);
        writer.WriteFieldHeader(fieldIdDelta, expectedType, typeof(LocalTime), WireType.Fixed64);
        writer.WriteInt64(value.TickOfDay);
    }

    public LocalTime ReadValue<TInput>(ref Reader<TInput> reader, Field field)
    {
        ReferenceCodec.MarkValueField(reader.Session);
        field.EnsureWireType(WireType.Fixed64);
        var tickOfDay = reader.ReadInt64();
        return LocalTime.FromTicksSinceMidnight(tickOfDay);
    }
}
