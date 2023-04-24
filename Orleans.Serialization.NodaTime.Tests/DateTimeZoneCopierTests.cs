using NodaTime;
using Orleans.Serialization.TestKit;
using Xunit.Abstractions;

namespace Orleans.Serialization.NodaTime.Tests;

public class DateTimeZoneCopierTests(ITestOutputHelper output) : CopierTester<DateTimeZone?, DateTimeZoneCopier>(output)
{
    protected override bool IsImmutable => true;

    protected override DateTimeZone? CreateValue() => DateTimeZone.Utc;

    protected override DateTimeZone?[] TestValues => new[]
    {
        DateTimeZone.Utc,
        DateTimeZoneProviders.Tzdb["Europe/Oslo"],
    };
}
