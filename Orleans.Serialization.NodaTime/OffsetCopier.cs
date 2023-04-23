using NodaTime;
using Orleans.Serialization.Cloning;

namespace Orleans.Serialization.NodaTime;

/// <summary>
/// Copier for <see cref="Offset"/>.
/// </summary>
[RegisterCopier]
public class OffsetCopier : IDeepCopier<Offset>
{
    public Offset DeepCopy(Offset input, CopyContext context)
    {
        return input;
    }
}
