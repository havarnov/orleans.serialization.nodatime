using System;
using System.Buffers;
using NodaTime;
using Orleans.Serialization.Buffers;
using Orleans.Serialization.Codecs;
using Orleans.Serialization.WireProtocol;

namespace Orleans.Serialization.NodaTime;

/// <summary>
/// Serializer for <see cref="Offset"/>.
/// </summary>
[RegisterSerializer]
public class OffsetCodec : IFieldCodec<Offset>
{
    public void WriteField<TBufferWriter>(
        ref Writer<TBufferWriter> writer,
        uint fieldIdDelta,
        Type expectedType,
        Offset value)
        where TBufferWriter : IBufferWriter<byte>
    {
        ReferenceCodec.MarkValueField(writer.Session);
        writer.WriteFieldHeader(fieldIdDelta, expectedType, typeof(Offset), WireType.VarInt);
        writer.WriteVarInt64(value.Ticks);
    }

    public Offset ReadValue<TInput>(ref Reader<TInput> reader, Field field)
    {
        ReferenceCodec.MarkValueField(reader.Session);
        field.EnsureWireType(WireType.VarInt);
        var ticks = reader.ReadVarInt64();
        return Offset.FromTicks(ticks);
    }
}
