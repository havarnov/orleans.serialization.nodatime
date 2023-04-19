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
        writer.WriteFieldHeader(fieldIdDelta, expectedType, typeof(LocalTime), WireType.VarInt);
        writer.WriteVarInt64(value.NanosecondOfDay);
    }

    public LocalTime ReadValue<TInput>(ref Reader<TInput> reader, Field field)
    {
        ReferenceCodec.MarkValueField(reader.Session);
        field.EnsureWireType(WireType.VarInt);
        var value = reader.ReadVarInt64();
        return LocalTime.FromNanosecondsSinceMidnight(value);
    }
}
