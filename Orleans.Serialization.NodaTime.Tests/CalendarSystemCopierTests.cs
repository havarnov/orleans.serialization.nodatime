using System.Linq;
using NodaTime;
using Orleans.Serialization.TestKit;
using Xunit.Abstractions;

namespace Orleans.Serialization.NodaTime.Tests;

public class CalendarSystemCopierTests: CopierTester<CalendarSystem?, CalendarSystemCopier>
{
    public CalendarSystemCopierTests(ITestOutputHelper output) : base(output)
    {
    }

    protected override bool IsImmutable => true;

    protected override CalendarSystem? CreateValue() => CalendarSystem.Iso;

    protected override CalendarSystem?[] TestValues =>
        CalendarSystem
            .Ids
            .Select(CalendarSystem.ForId)
            .ToArray();
}
