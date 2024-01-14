using System;
using NodaTime;
using Orleans.Serialization.Cloning;

namespace Orleans.Serialization.NodaTime;

/// <summary>
/// Copier for <see cref="DateInterval"/>.
/// </summary>
[RegisterCopier]
public class DateIntervalCopier : IDeepCopier<DateInterval?>, IGeneralizedCopier
{
    public DateInterval? DeepCopy(DateInterval? input, CopyContext context)
    {
        // Since LocalDate is an immutable class we can just return the input value directly.
        return input;
    }

    public bool IsSupportedType(Type type) => typeof(DateInterval).IsAssignableFrom(type);
}
