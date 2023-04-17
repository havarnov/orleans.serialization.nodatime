using NodaTime;
using Orleans.Serialization.TestKit;
using Xunit.Abstractions;

namespace Orleans.Serialization.NodaTime.Tests;

public class LocalTimeCodecTests : FieldCodecTester<LocalTime, LocalTimeCodec>
{
    public LocalTimeCodecTests(ITestOutputHelper output)
        : base(output)
    {
    }

    protected override LocalTime CreateValue() => LocalTime.Noon;

    protected override LocalTime[] TestValues => new[]
    {
        LocalTime.Midnight,
        LocalTime.Noon,
    };
}
