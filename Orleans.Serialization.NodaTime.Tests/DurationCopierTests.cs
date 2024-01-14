using NodaTime;
using Orleans.Serialization.TestKit;
using Xunit.Abstractions;

namespace Orleans.Serialization.NodaTime.Tests;

public class DurationCopierTests(ITestOutputHelper output)
    : CopierTester<Duration, DurationCopier>(output)
{
    protected override Duration CreateValue() => Duration.Epsilon;

    protected override Duration[] TestValues =>
    [
        Duration.Zero,
        Duration.Epsilon,
        Duration.MaxValue,
        Duration.MinValue,
        Duration.FromHours(2),
    ];
}