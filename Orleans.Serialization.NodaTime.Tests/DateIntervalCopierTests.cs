using NodaTime;
using Orleans.Serialization.TestKit;
using Xunit.Abstractions;

namespace Orleans.Serialization.NodaTime.Tests;

public class DateIntervalCopierTests(ITestOutputHelper output) : CopierTester<DateInterval?, DateIntervalCopier>(output)
{
    protected override bool IsImmutable => true;

    protected override DateInterval CreateValue() =>
        new DateInterval(LocalDate.MinIsoValue, LocalDate.MaxIsoValue);

    protected override DateInterval?[] TestValues =>
    [
        null,
        new DateInterval(LocalDate.MinIsoValue, LocalDate.MaxIsoValue),
        new DateInterval(
            LocalDate.MinIsoValue,
            new LocalDate(2024, 01, 14)),
        new DateInterval(
            new LocalDate(2024, 01, 14),
            LocalDate.MaxIsoValue),
        new DateInterval(
            new LocalDate(2024, 01, 14),
            new LocalDate(2024, 01, 14)),
    ];
}
