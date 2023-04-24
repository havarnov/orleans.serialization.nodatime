using System;
using NodaTime;
using Orleans.Serialization.Cloning;

namespace Orleans.Serialization.NodaTime;

/// <summary>
/// Copier for <see cref="CalendarSystem"/>.
/// </summary>
[RegisterCopier]
public class CalendarSystemCopier: IDeepCopier<CalendarSystem?>, IGeneralizedCopier
{
    public CalendarSystem? DeepCopy(CalendarSystem? input, CopyContext context)
    {
        return input;
    }

    public bool IsSupportedType(Type type) => typeof(CalendarSystem).IsAssignableFrom(type);
}
