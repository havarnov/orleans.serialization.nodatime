using NodaTime;
using Orleans.Serialization.Cloning;

namespace Orleans.Serialization.NodaTime;

[RegisterCopier]
public class CalendarSystemCopier: IDeepCopier<CalendarSystem?>
{
    public CalendarSystem? DeepCopy(CalendarSystem? input, CopyContext context)
    {
        return input;
    }
}
