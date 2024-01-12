using System;

namespace Orleans.Serialization.NodaTime;

public class NodaTimeCodecException : Exception
{
    public NodaTimeCodecException()
        : base()
    {
    }

    public NodaTimeCodecException(string message)
        : base(message)
    {
    }

    public NodaTimeCodecException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
