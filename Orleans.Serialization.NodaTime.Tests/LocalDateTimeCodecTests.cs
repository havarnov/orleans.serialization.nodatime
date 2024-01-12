using System;
using Microsoft.Extensions.DependencyInjection;
using NodaTime;
using Orleans.Serialization.Codecs;
using Orleans.Serialization.TestKit;
using Xunit.Abstractions;

namespace Orleans.Serialization.NodaTime.Tests;

public class LocalDateTimeCodecTests(ITestOutputHelper output)
    : FieldCodecTester<LocalDateTime, LocalDateTimeCodec>(output)
{
    protected override void Configure(ISerializerBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddSingleton<IFieldCodec<LocalDate>, LocalDateCodec>();
        builder.Services.AddSingleton<IFieldCodec<LocalTime>, LocalTimeCodec>();
        base.Configure(builder);
    }

    protected override LocalDateTime CreateValue() =>
        new LocalDateTime(2024, 01, 12, 13, 42, 56, 998);

    protected override LocalDateTime[] TestValues =>
    [
        new LocalDateTime(2024, 01, 12, 13, 42, 56, 998),
        LocalDateTime.MaxIsoValue,
        LocalDateTime.MinIsoValue,
    ];
}
