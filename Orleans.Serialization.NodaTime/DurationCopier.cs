using System;
using NodaTime;
using Orleans.Serialization.Cloning;

namespace Orleans.Serialization.NodaTime;

/// <summary>
/// Copier for <see cref="Duration"/>
/// </summary>
[RegisterCopier]
public class DurationCopier : IDeepCopier<Duration>
{
    public Duration DeepCopy(Duration input, CopyContext context)
    {
        // Since Duration is an immutable struct we can just return the input value directly.
        return input;
    }
}
