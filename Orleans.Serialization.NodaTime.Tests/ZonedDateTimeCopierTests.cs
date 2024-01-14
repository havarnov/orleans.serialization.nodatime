using NodaTime;
using Orleans.Serialization.TestKit;
using Xunit.Abstractions;

namespace Orleans.Serialization.NodaTime.Tests;

public class ZonedDateTimeCopierTests(ITestOutputHelper output)
    : CopierTester<ZonedDateTime, ZonedDateTimeCopier>(output)
{
    protected override ZonedDateTime CreateValue()
        => new ZonedDateTime(
            Instant.FromUtc(2024, 01, 12, 13, 13, 13),
            DateTimeZoneProviders.Tzdb["Europe/Oslo"]);

    protected override ZonedDateTime[] TestValues =>
    [
        CreateValue(),
    ];
}