using Microsoft.Extensions.DependencyInjection;
using NodaTime;
using Orleans.Serialization.Cloning;
using Orleans.Serialization.Serializers;
using Orleans.Serialization.TestKit;
using Xunit.Abstractions;

namespace Orleans.Serialization.NodaTime.Tests;

public class DateTimeZoneCodecTests : FieldCodecTester<DateTimeZone?, DateTimeZoneCodec>
{
    public DateTimeZoneCodecTests(ITestOutputHelper output) : base(output)
    {
    }

    protected override void Configure(ISerializerBuilder builder)
    {
        builder.Services.AddSingleton<IGeneralizedCopier, DateTimeZoneCopier>();
        builder.Services.AddSingleton<IGeneralizedCodec, DateTimeZoneCodec>();
        base.Configure(builder);
    }

    protected override DateTimeZone CreateValue() => DateTimeZone.Utc;

    protected override DateTimeZone[] TestValues => new[]
    {
        DateTimeZoneProviders.Tzdb["Europe/Oslo"],
    };
}
