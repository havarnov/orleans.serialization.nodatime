using System;
using Microsoft.Extensions.DependencyInjection;
using NodaTime;
using Orleans.Serialization.Cloning;
using Orleans.Serialization.Serializers;
using Orleans.Serialization.TestKit;
using Xunit.Abstractions;

namespace Orleans.Serialization.NodaTime.Tests;

public class DateTimeZoneCodecTests(ITestOutputHelper output)
    : FieldCodecTester<DateTimeZone?, DateTimeZoneCodec>(output)
{
    protected override void Configure(ISerializerBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddSingleton<IGeneralizedCopier, DateTimeZoneCopier>();
        builder.Services.AddSingleton<IGeneralizedCodec, DateTimeZoneCodec>();
        base.Configure(builder);
    }

    protected override DateTimeZone CreateValue() => DateTimeZone.Utc;

    protected override DateTimeZone?[] TestValues =>
    [
        null,
        DateTimeZoneProviders.Tzdb["Europe/Oslo"],
        DateTimeZoneProviders.Bcl["Europe/Oslo"],
        DateTimeZoneProviders.Bcl.GetSystemDefault()
    ];
}
