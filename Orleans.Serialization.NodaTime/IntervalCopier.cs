using NodaTime;
using Orleans.Serialization.Cloning;

namespace Orleans.Serialization.NodaTime;

/// <summary>
/// Copier for <see cref="Interval"/>.
/// </summary>
[RegisterCopier]
public class IntervalCopier : IDeepCopier<Interval>
{
    public Interval DeepCopy(Interval input, CopyContext context)
    {
        // Since Interval is an immutable struct we can just return the input value directly.
        return input;
    }
}
