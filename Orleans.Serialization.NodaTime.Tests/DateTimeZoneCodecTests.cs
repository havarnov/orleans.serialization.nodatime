using NodaTime;
using Orleans.Serialization.TestKit;
using Xunit.Abstractions;

namespace Orleans.Serialization.NodaTime.Tests;

public class DateTimeZoneCodecTests : FieldCodecTester<DateTimeZone?, DateTimeZoneCodec>
{
    public DateTimeZoneCodecTests(ITestOutputHelper output) : base(output)
    {
    }

    protected override DateTimeZone CreateValue() => DateTimeZone.Utc;

    protected override DateTimeZone[] TestValues => new[]
    {
        DateTimeZoneProviders.Tzdb["Europe/Oslo"],
    };
}
