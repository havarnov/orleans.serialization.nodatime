using NodaTime;
using Orleans.Serialization.TestKit;
using Xunit.Abstractions;

namespace Orleans.Serialization.NodaTime.Tests;

public class OffsetCodecTests(ITestOutputHelper output)
    : FieldCodecTester<Offset, OffsetCodec>(output)
{
    protected override Offset CreateValue() => Offset.Zero;

    protected override Offset[] TestValues => new[]
    {
        Offset.Zero,
        Offset.MaxValue,
        Offset.MinValue,
    };
}
