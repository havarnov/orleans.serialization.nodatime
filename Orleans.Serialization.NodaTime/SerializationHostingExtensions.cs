using System;
using Microsoft.Extensions.DependencyInjection;

namespace Orleans.Serialization.NodaTime;

public static class SerializationHostingExtensions
{
    public static ISerializerBuilder AddNodaTimeSerializers(this ISerializerBuilder serializerBuilder)
    {
        ArgumentNullException.ThrowIfNull(serializerBuilder);

        // Instant
        serializerBuilder.Configure(config => config.Copiers.Add(typeof(InstantCopier)));
        serializerBuilder.Services.AddSingleton<InstantCopier>();

        serializerBuilder.Configure(config => config.FieldCodecs.Add(typeof(InstantCodec)));
        serializerBuilder.Services.AddSingleton<InstantCodec>();

        // Offset
        serializerBuilder.Configure(config => config.Copiers.Add(typeof(OffsetCopier)));
        serializerBuilder.Services.AddSingleton<OffsetCopier>();

        serializerBuilder.Configure(config => config.FieldCodecs.Add(typeof(OffsetCodec)));
        serializerBuilder.Services.AddSingleton<OffsetCodec>();

        // CalendarSystem
        serializerBuilder.Configure(config => config.Copiers.Add(typeof(CalendarSystemCopier)));
        serializerBuilder.Services.AddSingleton<CalendarSystemCopier>();

        serializerBuilder.Configure(config => config.FieldCodecs.Add(typeof(CalendarSystemCodec)));
        serializerBuilder.Services.AddSingleton<CalendarSystemCodec>();

        // LocalDateTime
        serializerBuilder.Configure(config => config.Copiers.Add(typeof(LocalDateTimeCopier)));
        serializerBuilder.Services.AddSingleton<LocalDateTimeCopier>();

        serializerBuilder.Configure(config => config.FieldCodecs.Add(typeof(LocalDateTimeCodec)));
        serializerBuilder.Services.AddSingleton<LocalDateTimeCodec>();

        // LocalDate
        serializerBuilder.Configure(config => config.Copiers.Add(typeof(LocalDateCopier)));
        serializerBuilder.Services.AddSingleton<LocalDateCopier>();

        serializerBuilder.Configure(config => config.FieldCodecs.Add(typeof(LocalDateCodec)));
        serializerBuilder.Services.AddSingleton<LocalDateCodec>();

        // LocalTime
        serializerBuilder.Configure(config => config.Copiers.Add(typeof(LocalTimeCopier)));
        serializerBuilder.Services.AddSingleton<LocalTimeCopier>();

        serializerBuilder.Configure(config => config.FieldCodecs.Add(typeof(LocalTimeCodec)));
        serializerBuilder.Services.AddSingleton<LocalTimeCodec>();

        // OffsetDateTime
        serializerBuilder.Configure(config => config.Copiers.Add(typeof(OffsetDateTimeCopier)));
        serializerBuilder.Services.AddSingleton<OffsetDateTimeCopier>();

        serializerBuilder.Configure(config => config.FieldCodecs.Add(typeof(OffsetDateTimeCodec)));
        serializerBuilder.Services.AddSingleton<OffsetDateTimeCodec>();

        // OffsetDate
        serializerBuilder.Configure(config => config.Copiers.Add(typeof(OffsetDateCopier)));
        serializerBuilder.Services.AddSingleton<OffsetDateCopier>();

        serializerBuilder.Configure(config => config.FieldCodecs.Add(typeof(OffsetDateCodec)));
        serializerBuilder.Services.AddSingleton<OffsetDateCodec>();

        // OffsetTime
        serializerBuilder.Configure(config => config.Copiers.Add(typeof(OffsetTimeCopier)));
        serializerBuilder.Services.AddSingleton<OffsetTimeCopier>();

        serializerBuilder.Configure(config => config.FieldCodecs.Add(typeof(OffsetTimeCodec)));
        serializerBuilder.Services.AddSingleton<OffsetTimeCodec>();

        // DateTimeZone
        serializerBuilder.Configure(config => config.Copiers.Add(typeof(DateTimeZoneCopier)));
        serializerBuilder.Services.AddSingleton<DateTimeZoneCopier>();

        serializerBuilder.Configure(config => config.FieldCodecs.Add(typeof(DateTimeZoneCodec)));
        serializerBuilder.Services.AddSingleton<DateTimeZoneCodec>();

        // ZonedDateTime
        serializerBuilder.Configure(config => config.Copiers.Add(typeof(ZonedDateTimeCopier)));
        serializerBuilder.Services.AddSingleton<ZonedDateTimeCopier>();

        serializerBuilder.Configure(config => config.FieldCodecs.Add(typeof(ZonedDateTimeCodec)));
        serializerBuilder.Services.AddSingleton<ZonedDateTimeCodec>();

        // Duration
        serializerBuilder.Configure(config => config.Copiers.Add(typeof(DurationCopier)));
        serializerBuilder.Services.AddSingleton<DurationCopier>();

        serializerBuilder.Configure(config => config.FieldCodecs.Add(typeof(DurationCodec)));
        serializerBuilder.Services.AddSingleton<DurationCodec>();

        // Period
        serializerBuilder.Configure(config => config.Copiers.Add(typeof(PeriodCopier)));
        serializerBuilder.Services.AddSingleton<PeriodCopier>();

        serializerBuilder.Configure(config => config.FieldCodecs.Add(typeof(PeriodCodec)));
        serializerBuilder.Services.AddSingleton<PeriodCodec>();

        // Interval
        serializerBuilder.Configure(config => config.Copiers.Add(typeof(IntervalCopier)));
        serializerBuilder.Services.AddSingleton<IntervalCopier>();

        serializerBuilder.Configure(config => config.FieldCodecs.Add(typeof(IntervalCodec)));
        serializerBuilder.Services.AddSingleton<IntervalCodec>();

        // DateInterval
        serializerBuilder.Configure(config => config.Copiers.Add(typeof(DateIntervalCopier)));
        serializerBuilder.Services.AddSingleton<DateIntervalCopier>();

        serializerBuilder.Configure(config => config.FieldCodecs.Add(typeof(DateIntervalCodec)));
        serializerBuilder.Services.AddSingleton<DateIntervalCodec>();

        return serializerBuilder;
    }
}
