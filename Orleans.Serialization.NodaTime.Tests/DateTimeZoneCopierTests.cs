using NodaTime;
using Orleans.Serialization.TestKit;
using Xunit.Abstractions;

namespace Orleans.Serialization.NodaTime.Tests;

public class DateTimeZoneCopierTests: CopierTester<DateTimeZone?, DateTimeZoneCopier>
{
    public DateTimeZoneCopierTests(ITestOutputHelper output) : base(output)
    {
    }

    protected override bool IsImmutable => true;

    protected override DateTimeZone? CreateValue() => DateTimeZone.Utc;

    protected override DateTimeZone?[] TestValues => new[]
    {
        DateTimeZone.Utc,
        DateTimeZoneProviders.Tzdb["Europe/Oslo"],
    };
}
