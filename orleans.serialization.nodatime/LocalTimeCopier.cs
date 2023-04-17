using NodaTime;
using Orleans.Serialization.Cloning;

namespace Orleans.Serialization.NodaTime;

/// <summary>
/// Copier for <see cref="LocalTime"/>.
/// </summary>
[RegisterCopier]
public class LocalTimeCopier : IDeepCopier<LocalTime>
{
    public LocalTime DeepCopy(LocalTime input, CopyContext context)
    {
        return LocalTime.FromTicksSinceMidnight(input.TickOfDay);
    }
}
