using NodaTime;
using Orleans.Serialization.Cloning;

namespace Orleans.Serialization.NodaTime;

/// <summary>
/// Copier for <see cref="Instant"/>.
/// </summary>
[RegisterCopier]
public class InstantCopier : IDeepCopier<Instant>
{
    public Instant DeepCopy(Instant input, CopyContext context)
    {
        // Since Instant is an immutable struct we can just return the input value directly.
        return input;
    }
}