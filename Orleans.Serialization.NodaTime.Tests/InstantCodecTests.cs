using NodaTime;
using Orleans.Serialization.TestKit;
using Xunit.Abstractions;

namespace Orleans.Serialization.NodaTime.Tests;

public class InstantCodecTests: FieldCodecTester<Instant, InstantCodec>
{
    public InstantCodecTests(ITestOutputHelper output) : base(output)
    {
    }

    protected override Instant CreateValue() =>
        SystemClock.Instance.GetCurrentInstant();

    protected override Instant[] TestValues => new[]
    {
        SystemClock.Instance.GetCurrentInstant(),
        Instant.MaxValue,
        Instant.MinValue,
    };
}
