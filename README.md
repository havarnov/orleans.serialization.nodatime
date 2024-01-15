Orleans.Serialization.NodaTime is a library that provides Orleans serialization functionality for NodaTime.

The library implements codecs and copiers for the following types:

- [x] Instant
- [x] Offset
- [x] CalendarSystem
- [x] LocalDateTime
- [x] LocalDate
- [x] LocalTime
- [x] OffsetDateTime
- [x] OffsetDate
- [x] OffsetTime
- [x] DateTimeZone
- [x] ZonedDateTime
- [x] Duration
- [x] Period
- [x] Interval
- [x] DateInterval

```csharp
await Host
    .CreateDefaultBuilder()
    .ConfigureServices((ctx, services) =>
    {
        services.AddOrleans(builder =>
        {
            builder.Services.AddSerializer(serializerBuilder =>
            {
                serializerBuilder.AddNodaTimeSerializers();
            })
        })
    })
    .UseConsoleLifetime()
    .Build()
    .RunAsync();
```
