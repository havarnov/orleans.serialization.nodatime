using NodaTime;
using Orleans.Serialization.TestKit;
using Xunit.Abstractions;

namespace Orleans.Serialization.NodaTime.Tests;

public class LocalDateCodecTests(ITestOutputHelper output)
    : FieldCodecTester<LocalDate, LocalDateCodec>(output)
{
    protected override LocalDate CreateValue() =>
        new LocalDate(2024, 01, 12);

    protected override LocalDate[] TestValues =>
    [
        LocalDate.MaxIsoValue,
        LocalDate.MinIsoValue,
        new LocalDate(2024, 01, 12),
    ];
}
