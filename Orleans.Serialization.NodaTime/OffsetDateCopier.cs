using System;
using NodaTime;
using Orleans.Serialization.Cloning;

namespace Orleans.Serialization.NodaTime;

[RegisterCopier]
public class OffsetDateCopier : IDeepCopier<OffsetDate>
{
    public OffsetDate DeepCopy(OffsetDate input, CopyContext context)
    {
        // Since LocalDate is an immutable struct we can just return the input value directly.
        return input;
    }
}
