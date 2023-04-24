using NodaTime;
using Orleans.Serialization.TestKit;
using Xunit.Abstractions;

namespace Orleans.Serialization.NodaTime.Tests;

public class LocalTimeCopierTests(ITestOutputHelper output)
    : CopierTester<LocalTime, LocalTimeCopier>(output)
{
    protected override LocalTime CreateValue() => LocalTime.Noon;

    protected override LocalTime[] TestValues => new[]
    {
        LocalTime.Midnight,
        LocalTime.Noon,
        LocalTime.MaxValue,
        LocalTime.MinValue,
    };
}
