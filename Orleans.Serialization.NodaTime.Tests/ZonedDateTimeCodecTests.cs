using System;
using Microsoft.Extensions.DependencyInjection;
using NodaTime;
using Orleans.Serialization.Cloning;
using Orleans.Serialization.TestKit;
using Xunit.Abstractions;

namespace Orleans.Serialization.NodaTime.Tests;

public class ZonedDateTimeCodecTests(ITestOutputHelper output)
    : FieldCodecTester<ZonedDateTime, ZonedDateTimeCodec>(output)
{
    protected override void Configure(ISerializerBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddSingleton<IGeneralizedCopier, ZonedDateTimeCopier>();
        base.Configure(builder);
    }

    protected override ZonedDateTime CreateValue()
        => new ZonedDateTime(
            Instant.FromUtc(2024, 01, 12, 13, 13, 13),
            DateTimeZoneProviders.Tzdb["Europe/Oslo"]);

    protected override ZonedDateTime[] TestValues =>
    [
        CreateValue(),
    ];
}
