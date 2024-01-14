using System;
using Microsoft.Extensions.DependencyInjection;
using NodaTime;
using Orleans.Serialization.Cloning;
using Orleans.Serialization.TestKit;
using Xunit.Abstractions;

namespace Orleans.Serialization.NodaTime.Tests;

public class PeriodCodecTests(ITestOutputHelper output)
    : FieldCodecTester<Period?, PeriodCodec>(output)
{
    protected override void Configure(ISerializerBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddSingleton<IGeneralizedCopier, PeriodCopier>();
        base.Configure(builder);
    }

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
