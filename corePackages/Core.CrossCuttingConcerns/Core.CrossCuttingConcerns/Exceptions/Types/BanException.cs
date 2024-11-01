using System.Runtime.Serialization;

namespace Core.CrossCuttingConcerns.Exceptions.Types;

public class BanException : Exception
{
    public BanException() { }

    protected BanException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }

    public BanException(string? message)
        : base(message) { }

    public BanException(string? message, Exception? innerException)
        : base(message, innerException) { }
}

