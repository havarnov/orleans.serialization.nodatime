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
/// Codec for <see cref="LocalDate"/>.
/// </summary>
[RegisterSerializer]
public class LocalDateCodec : IFieldCodec<LocalDate>
{
    public void WriteField<TBufferWriter>(
        ref Writer<TBufferWriter> writer,
        uint fieldIdDelta,
        Type expectedType,
        LocalDate value)
        where TBufferWriter : IBufferWriter<byte>
    {
        var valueAsString = LocalDatePattern.FullRoundtrip.Format(value);
        var bytes = Encoding.UTF8.GetBytes(valueAsString);

        ReferenceCodec.MarkValueField(writer.Session);
        writer.WriteFieldHeader(fieldIdDelta, expectedType, typeof(LocalDate), WireType.LengthPrefixed);
        writer.WriteVarUInt32((uint)bytes.Length);
        writer.Write(bytes);
    }

    public LocalDate ReadValue<TInput>(ref Reader<TInput> reader, Field field)
    {
        ReferenceCodec.MarkValueField(reader.Session);
        field.EnsureWireType(WireType.LengthPrefixed);
        var length = reader.ReadVarUInt32();
        var buffer = reader.ReadBytes(length);
        var valueAsStr = Encoding.UTF8.GetString(buffer);
        var parseResult = LocalDatePattern.FullRoundtrip.Parse(valueAsStr);
        if (!parseResult.Success)
        {
            throw new NodaTimeCodecException(
                $"Couldn't parse {valueAsStr} as {nameof(LocalDate)} with pattern {LocalDatePattern.FullRoundtrip.PatternText}.",
                parseResult.Exception);
        }

        return parseResult.Value;
    }
}
