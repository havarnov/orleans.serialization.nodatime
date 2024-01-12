using NodaTime;
using Orleans.Serialization.TestKit;
using Xunit.Abstractions;

namespace Orleans.Serialization.NodaTime.Tests;

public class OffsetDateCopierTests(ITestOutputHelper output)
    : CopierTester<OffsetDate, OffsetDateCopier>(output)
{
    protected override OffsetDate CreateValue() =>
        new OffsetDate(new LocalDate(2024, 01, 12), Offset.FromHours(3));

    protected override OffsetDate[] TestValues =>
    [
        new OffsetDate(new LocalDate(2024, 01, 12), Offset.FromHours(3)),
        new OffsetDate(new LocalDate(2024, 01, 01), Offset.FromHours(1)),
    ];
}
