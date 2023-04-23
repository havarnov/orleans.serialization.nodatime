using System;
using System.Buffers;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using NodaTime;
using NodaTime.Text;
using Orleans.Serialization.Buffers;
using Orleans.Serialization.Codecs;
using Orleans.Serialization.WireProtocol;

namespace Orleans.Serialization.NodaTime;

/// <summary>
/// Serializer for <see cref="Instant"/>.
/// </summary>
[RegisterSerializer]
public class InstantCodec : IFieldCodec<Instant>
{
    public void WriteField<TBufferWriter>(
        ref Writer<TBufferWriter> writer,
        uint fieldIdDelta,
        Type expectedType,
        Instant value)
        where TBufferWriter : IBufferWriter<byte>
    {
        ReferenceCodec.MarkValueField(writer.Session);
        // We must serialize an Instant as a serialized string with the ExtendedIso pattern
        // to be able to send it with maximum precision,
        // see https://nodatime.org/2.0.x/api/NodaTime.Text.InstantPattern.html#NodaTime_Text_InstantPattern_ExtendedIso.
        var instantStr = InstantPattern.ExtendedIso.Format(value);
        writer.WriteFieldHeader(fieldIdDelta, expectedType, typeof(Instant), WireType.LengthPrefixed);
        var bytes = Encoding.UTF8.GetBytes(instantStr);
        writer.WriteVarUInt32((uint)bytes.Length);
        writer.Write(bytes);
    }

    public Instant ReadValue<TInput>(ref Reader<TInput> reader, Field field)
    {
        ReferenceCodec.MarkValueField(reader.Session);
        field.EnsureWireType(WireType.LengthPrefixed);
        var length = reader.ReadVarUInt32();
        var buffer = reader.ReadBytes(length);
        var instantStr = Encoding.UTF8.GetString(buffer);
        var parseResult = InstantPattern.ExtendedIso.Parse(instantStr);
        return parseResult.Value;
    }
}
