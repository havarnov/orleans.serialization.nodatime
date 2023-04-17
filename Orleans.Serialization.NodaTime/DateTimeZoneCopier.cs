using System;
using NodaTime;
using Orleans.Serialization.Cloning;

namespace Orleans.Serialization.NodaTime;

/// <summary>
/// Copier for <see cref="DateTimeZone"/>.
/// </summary>
[RegisterCopier]
public class DateTimeZoneCopier: IDeepCopier<DateTimeZone>, IGeneralizedCopier
{
    public DateTimeZone DeepCopy(DateTimeZone input, CopyContext context)
    {
        return input;
    }

    public bool IsSupportedType(Type type)
    {
        return typeof(DateTimeZone).IsAssignableFrom(type);
    }
}
