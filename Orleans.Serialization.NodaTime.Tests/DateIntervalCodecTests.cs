using System;
using Microsoft.Extensions.DependencyInjection;
using NodaTime;
using NodaTime.TimeZones;
using Orleans.Serialization.Cloning;
using Orleans.Serialization.TestKit;
using Xunit.Abstractions;

namespace Orleans.Serialization.NodaTime.Tests;

public class DateIntervalCodecTests(ITestOutputHelper output)
    : FieldCodecTester<DateInterval?, DateIntervalCodec>(output)
{
    protected override void Configure(ISerializerBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddSingleton<IGeneralizedCopier, DateIntervalCopier>();
        base.Configure(builder);
    }

    protected override DateInterval CreateValue() =>
        new DateInterval(LocalDate.MinIsoValue, LocalDate.MaxIsoValue);

    protected override DateInterval?[] TestValues =>
    [
        null,
        new DateInterval(LocalDate.MinIsoValue, LocalDate.MaxIsoValue),
        new DateInterval(
            LocalDate.MinIsoValue,
            new LocalDate(2024, 01, 14)),
        new DateInterval(
            new LocalDate(2024, 01, 14),
            LocalDate.MaxIsoValue),
        new DateInterval(
            new LocalDate(2024, 01, 14),
            new LocalDate(2024, 01, 14)),
    ];
}
