using NodaTime;
using Orleans.Serialization.TestKit;
using Xunit.Abstractions;

namespace Orleans.Serialization.NodaTime.Tests;

public class InstantCopierTests(ITestOutputHelper output)
    : CopierTester<Instant, InstantCopier>(output)
{
    protected override Instant CreateValue() =>
        SystemClock.Instance.GetCurrentInstant();

    protected override Instant[] TestValues => new[]
    {
        SystemClock.Instance.GetCurrentInstant(),
        Instant.MaxValue,
        Instant.MinValue,
    };
}
