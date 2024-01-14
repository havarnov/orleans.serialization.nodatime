using System;
using NodaTime;
using Orleans.Serialization.Cloning;

namespace Orleans.Serialization.NodaTime;

/// <summary>
/// Copier for <see cref="Period"/>
/// </summary>
[RegisterCopier]
public class PeriodCopier : IDeepCopier<Period?>, IGeneralizedCopier
{
    public Period? DeepCopy(Period? input, CopyContext context)
    {
        // Since Period is an immutable class we can just return the input value directly.
        return input;
    }

    public bool IsSupportedType(Type type) => typeof(Period).IsAssignableFrom(type);
}
