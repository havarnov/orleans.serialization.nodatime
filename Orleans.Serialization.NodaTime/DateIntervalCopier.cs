using NodaTime;
using Orleans.Serialization.Cloning;

namespace Orleans.Serialization.NodaTime;

/// <summary>
/// Copier for <see cref="DateInterval"/>.
/// </summary>
[RegisterCopier]
public class DateIntervalCopier : IDeepCopier<DateInterval>
{
    public DateInterval DeepCopy(DateInterval input, CopyContext context)
    {
        // Since DateInterval is an immutable struct we can just return the input value directly.
        return input;
    }
}
