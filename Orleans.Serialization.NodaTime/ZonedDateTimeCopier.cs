using System;
using NodaTime;
using Orleans.Serialization.Cloning;

namespace Orleans.Serialization.NodaTime;

[RegisterCopier]
public class ZonedDateTimeCopier : IDeepCopier<ZonedDateTime>, IGeneralizedCopier
{
    public ZonedDateTime DeepCopy(ZonedDateTime input, CopyContext context)
    {
        // Since LocalDate is an immutable struct we can just return the input value directly.
        return input;
    }

    public bool IsSupportedType(Type type) => typeof(ZonedDateTime).IsAssignableFrom(type);
}
