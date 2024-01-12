using NodaTime;
using Orleans.Serialization.TestKit;
using Xunit.Abstractions;

namespace Orleans.Serialization.NodaTime.Tests;

public class OffsetDateTimeCodecTests(ITestOutputHelper output)
    : FieldCodecTester<OffsetDateTime, OffsetDateTimeCodec>(output)
{
    protected override OffsetDateTime CreateValue() =>
        new OffsetDateTime(
            new LocalDate(2014, 01, 12).AtMidnight(),
            Offset.FromMilliseconds(178));

    protected override OffsetDateTime[] TestValues =>
    [
        CreateValue(),
    ];
}
