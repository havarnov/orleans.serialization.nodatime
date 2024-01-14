using System;
using NodaTime;
using Orleans.Serialization.TestKit;
using Xunit.Abstractions;

namespace Orleans.Serialization.NodaTime.Tests;

public class PeriodCopierTests(ITestOutputHelper output)
    : CopierTester<Period?, PeriodCopier>(output)
{

    protected override bool IsImmutable => true;

    protected override Period CreateValue() => Period.Zero;

    protected override Period?[] TestValues =>
    [
        null,
        Period.Zero,
        Period.FromTicks(TimeSpan.MaxValue.Ticks),
        Period.FromTicks(TimeSpan.MinValue.Ticks),
        Period.FromDays(23),
    ];
}
