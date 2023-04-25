using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using NodaTime;
using Orleans.Serialization.Cloning;
using Orleans.Serialization.TestKit;
using Xunit.Abstractions;

namespace Orleans.Serialization.NodaTime.Tests;

public class CalendarSystemCodecTests: FieldCodecTester<CalendarSystem?, CalendarSystemCodec>
{
    public CalendarSystemCodecTests(ITestOutputHelper output) : base(output)
    {
    }

    protected override void Configure(ISerializerBuilder builder)
    {
        // builder.Services.AddSingleton<IGeneralizedCopier, CalendarSystemCopier>();
        base.Configure(builder);
    }

    protected override CalendarSystem? CreateValue() => CalendarSystem.Iso;

    protected override CalendarSystem?[] TestValues =>
        CalendarSystem
            .Ids
            .Select(CalendarSystem.ForId)
            .ToArray();
}
