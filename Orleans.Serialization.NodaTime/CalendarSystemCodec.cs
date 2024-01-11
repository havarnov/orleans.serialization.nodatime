using System;
using System.Buffers;
using System.Diagnostics;
using System.Text;
using NodaTime;
using Orleans.Serialization.Buffers;
using Orleans.Serialization.Codecs;
using Orleans.Serialization.WireProtocol;

namespace Orleans.Serialization.NodaTime;

/// <summary>
/// Serializer for <see cref="CalendarSystem"/>.
/// </summary>
[RegisterSerializer]
public class CalendarSystemCodec : IFieldCodec<CalendarSystem?>
{
    public void WriteField<TBufferWriter>(
        ref Writer<TBufferWriter> writer,
        uint fieldIdDelta,
        Type expectedType,
        CalendarSystem? value)
        where TBufferWriter : IBufferWriter<byte>
    {
        if (ReferenceCodec.TryWriteReferenceField(ref writer, fieldIdDelta, expectedType, value))
        {
            return;
        }

        // 'value' can't be null since ReferenceCodec.TryWriteReferenceField would always be able to write a 'null' field.
        Debug.Assert(value is not null);

        writer.WriteFieldHeader(fieldIdDelta, expectedType, typeof(CalendarSystem), WireType.LengthPrefixed);
        var bytes = Encoding.UTF8.GetBytes(value.Id);
        writer.WriteVarUInt32((uint)bytes.Length);
        writer.Write(bytes);
    }

    public CalendarSystem? ReadValue<TInput>(ref Reader<TInput> reader, Field field)
    {
        if (field.WireType == WireType.Reference)
        {
            return ReferenceCodec.ReadReference<CalendarSystem, TInput>(ref reader, field);
        }

        field.EnsureWireType(WireType.LengthPrefixed);
        var length = reader.ReadVarUInt32();
        var buffer = reader.ReadBytes(length);
        var id = Encoding.UTF8.GetString(buffer);
        var value = CalendarSystem.ForId(id);
        ReferenceCodec.RecordObject(reader.Session, value);
        return value;
    }
}
