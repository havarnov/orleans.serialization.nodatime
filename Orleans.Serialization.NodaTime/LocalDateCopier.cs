using System;
using NodaTime;
using Orleans.Serialization.Cloning;

namespace Orleans.Serialization.NodaTime;

/// <summary>
/// Copier for <see cref="LocalDate"/>.
/// </summary>
[RegisterCopier]
public class LocalDateCopier : IDeepCopier<LocalDate>
{
    public LocalDate DeepCopy(LocalDate input, CopyContext context)
    {
        // Since LocalDate is an immutable struct we can just return the input value directly.
        return input;
    }
}
