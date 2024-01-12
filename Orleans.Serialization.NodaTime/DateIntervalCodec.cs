using NodaTime;
using NodaTime.Text;
using Orleans.Serialization.Codecs;
using Orleans.Serialization.WireProtocol;
using System;
using System.Buffers;
using System.Text;

namespace Orleans.Serialization.NodaTime;

/// <summary>
/// Codec for <see cref="DateInterval"/>.
/// </summary>
[RegisterSerializer]
public class DateIntervalCodec : IFieldCodec<DateInterval>
{
    public void WriteField<TBufferWriter>(
        ref Buffers.Writer<TBufferWriter> writer,
        uint fieldIdDelta,
        Type expectedType,
        DateInterval value)
        where TBufferWriter : IBufferWriter<byte>
    {
        ArgumentNullException.ThrowIfNull(value, nameof(value));

        var startAsString = LocalDatePattern.FullRoundtrip.Format(value.Start);
        var endAsString = LocalDatePattern.Iso.Format(value.End);
        var bytes = Encoding.UTF8.GetBytes(startAsString + '!' + endAsString);

        ReferenceCodec.MarkValueField(writer.Session);
        writer.WriteFieldHeader(fieldIdDelta, expectedType, typeof(DateInterval), WireType.LengthPrefixed);
        writer.WriteVarUInt32((uint)bytes.Length);
        writer.Write(bytes);
    }

    public DateInterval ReadValue<TInput>(ref Buffers.Reader<TInput> reader, Field field)
    {
        ReferenceCodec.MarkValueField(reader.Session);
        field.EnsureWireType(WireType.LengthPrefixed);
        var length = reader.ReadVarUInt32();
        var buffer = reader.ReadBytes(length);
        var valueAsStr = Encoding.UTF8.GetString(buffer);
        var partsAsStr = valueAsStr.Split('!');
        if (partsAsStr.Length != 2)
        {
            throw new NodaTimeCodecException(
                $"Couldn't understand {valueAsStr} as two LocalDates separated by '!'");
        }

        var parseStart = LocalDatePattern.FullRoundtrip.Parse(partsAsStr[0]);
        if (!parseStart.Success)
        {
            throw new NodaTimeCodecException(
                $"Couldn't parse {partsAsStr[0]} as {nameof(LocalDate)} with pattern {LocalDatePattern.FullRoundtrip.PatternText}.",
                parseStart.Exception);
        }

        var parseEnd = LocalDatePattern.Iso.WithCalendar(parseStart.Value.Calendar).Parse(partsAsStr[1]);
        if (!parseEnd.Success)
        {
            throw new NodaTimeCodecException(
                $"Couldn't parse {partsAsStr[1]} as {nameof(LocalDate)} with pattern {LocalDatePattern.Iso.PatternText}.",
                parseEnd.Exception);
        }

        return new DateInterval(parseStart.Value, parseEnd.Value);
    }
}
