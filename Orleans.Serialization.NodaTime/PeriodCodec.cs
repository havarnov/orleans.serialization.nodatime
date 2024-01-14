using System;
using System.Buffers;
using System.Diagnostics;
using System.Text;
using NodaTime;
using NodaTime.Text;
using Orleans.Serialization.Buffers;
using Orleans.Serialization.Codecs;
using Orleans.Serialization.WireProtocol;

namespace Orleans.Serialization.NodaTime;

/// <summary>
/// Serializer for <see cref="Period"/>
/// </summary>
[RegisterSerializer]
public class PeriodCodec : IFieldCodec<Period?>
{
    public void WriteField<TBufferWriter>(
        ref Writer<TBufferWriter> writer,
        uint fieldIdDelta,
        Type expectedType,
        Period? value)
        where TBufferWriter : IBufferWriter<byte>
    {
        if (ReferenceCodec.TryWriteReferenceField(ref writer, fieldIdDelta, expectedType, value))
        {
            return;
        }

        // 'value' can't be null since ReferenceCodec.TryWriteReferenceField would always be able to write a 'null' field.
        Debug.Assert(value is not null);

        writer.WriteFieldHeader(fieldIdDelta, expectedType, typeof(Period), WireType.LengthPrefixed);
        var bytes = Encoding.UTF8.GetBytes(PeriodPattern.Roundtrip.Format(value));
        writer.WriteVarUInt32((uint)bytes.Length);
        writer.Write(bytes);
    }

    public Period? ReadValue<TInput>(
        ref Reader<TInput> reader,
        Field field)
    {
        if (field.WireType == WireType.Reference)
        {
            return ReferenceCodec.ReadReference<Period, TInput>(ref reader, field);
        }

        field.EnsureWireType(WireType.LengthPrefixed);
        var length = reader.ReadVarUInt32();
        var buffer = reader.ReadBytes(length);
        var periodStr = Encoding.UTF8.GetString(buffer);
        var parseResult = PeriodPattern.Roundtrip.Parse(periodStr);
        if (!parseResult.Success)
        {
            throw new NodaTimeCodecException(
                $"Couldn't parse {periodStr} as {nameof(Period)} with pattern {PeriodPattern.Roundtrip}.",
                parseResult.Exception);
        }

        var value = parseResult.Value;
        ReferenceCodec.RecordObject(reader.Session, value);
        return value;
    }
}
