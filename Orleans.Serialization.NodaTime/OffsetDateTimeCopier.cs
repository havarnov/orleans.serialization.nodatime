using System;
using NodaTime;
using Orleans.Serialization.Cloning;

namespace Orleans.Serialization.NodaTime;

[RegisterCopier]
public class OffsetDateTimeCopier : IDeepCopier<OffsetDateTime>
{
    public OffsetDateTime DeepCopy(OffsetDateTime input, CopyContext context)
    {
        // Since LocalDate is an immutable struct we can just return the input value directly.
        return input;
    }
}
