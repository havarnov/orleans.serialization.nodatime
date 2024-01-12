using NodaTime;
using Orleans.Serialization.Cloning;

namespace Orleans.Serialization.NodaTime;

/// <summary>
/// Copier for <see cref="LocalDateTime"/>.
/// </summary>
[RegisterCopier]
public class LocalDateTimeCopier : IDeepCopier<LocalDateTime>
{
    public LocalDateTime DeepCopy(LocalDateTime input, CopyContext context)
    {
        // Since LocalDate is an immutable struct we can just return the input value directly.
        return input;
    }
}