using System;
using System.Buffers;
using System.Text;
using NodaTime;
using NodaTime.Text;
using Orleans.Serialization.Buffers;
using Orleans.Serialization.Codecs;
using Orleans.Serialization.WireProtocol;

namespace Orleans.Serialization.NodaTime;

/// <summary>
/// Serializer for <see cref="Duration"/>.
/// </summary>
[RegisterSerializer]
public class DurationCodec : IFieldCodec<Duration>
{
    public void WriteField<TBufferWriter>(
        ref Writer<TBufferWriter> writer,
        uint fieldIdDelta,
        Type expectedType,
        Duration value)
        where TBufferWriter : IBufferWriter<byte>
    {
        ReferenceCodec.MarkValueField(writer.Session);
        writer.WriteFieldHeader(fieldIdDelta, expectedType, typeof(Duration), WireType.LengthPrefixed);
        var bytes = Encoding.UTF8.GetBytes(DurationPattern.Roundtrip.Format(value));
        writer.WriteVarUInt32((uint)bytes.Length);
        writer.Write(bytes);
    }

    public Duration ReadValue<TInput>(
        ref Reader<TInput> reader,
        Field field)
    {
        ReferenceCodec.MarkValueField(reader.Session);
        field.EnsureWireType(WireType.LengthPrefixed);
        var length = reader.ReadVarUInt32();
        var buffer = reader.ReadBytes(length);
        var durationStr = Encoding.UTF8.GetString(buffer);
        var parseResult = DurationPattern.Roundtrip.Parse(durationStr);
        if (!parseResult.Success)
        {
            throw new NodaTimeCodecException(
                $"Couldn't parse {durationStr} as {nameof(Duration)} with pattern {DurationPattern.Roundtrip.PatternText}.",
                parseResult.Exception);
        }

        return parseResult.Value;
    }
}
