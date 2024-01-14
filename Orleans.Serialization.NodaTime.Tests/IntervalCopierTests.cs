using NodaTime;
using Orleans.Serialization.TestKit;
using Xunit.Abstractions;

namespace Orleans.Serialization.NodaTime.Tests;

public class IntervalCopierTests(ITestOutputHelper output)
    : CopierTester<Interval, IntervalCopier>(output)
{
    protected override Interval CreateValue() =>
        new Interval(Instant.MinValue, Instant.MaxValue);

    protected override Interval[] TestValues =>
    [
        new Interval(Instant.MinValue, Instant.MaxValue),
        new Interval(
            Instant.FromUtc(2024, 01, 14, 13, 03, 03),
            Instant.MaxValue),
        new Interval(
            Instant.MinValue,
            Instant.FromUtc(2024, 01, 14, 13, 03, 03)),
        new Interval(
            Instant.FromUtc(2024, 01, 14, 13, 03, 03),
            Instant.FromUtc(2024, 01, 14, 13, 03, 03)),
    ];
}