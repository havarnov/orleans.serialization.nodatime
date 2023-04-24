using NodaTime;
using Orleans.Serialization.TestKit;
using Xunit.Abstractions;

namespace Orleans.Serialization.NodaTime.Tests;

public class InstantCodecTests(ITestOutputHelper output)
    : FieldCodecTester<Instant, InstantCodec>(output)
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
