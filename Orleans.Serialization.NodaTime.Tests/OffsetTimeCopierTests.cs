using NodaTime;
using Orleans.Serialization.TestKit;
using Xunit.Abstractions;

namespace Orleans.Serialization.NodaTime.Tests;

public class OffsetTimeCopierTests(ITestOutputHelper output)
    : CopierTester<OffsetTime, OffsetTimeCopier>(output)
{
    protected override OffsetTime CreateValue() =>
        new OffsetTime(new LocalTime(23, 01, 12), Offset.FromHours(1));

    protected override OffsetTime[] TestValues =>
    [
        new OffsetTime(new LocalTime(23, 01, 12), Offset.FromHours(1)),
        new OffsetTime(LocalTime.Midnight, Offset.FromHours(-1)),
        new OffsetTime(LocalTime.Midnight, Offset.FromHours(2)),
    ];
}
