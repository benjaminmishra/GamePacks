namespace GamePacks.Service.Models;

public record PackError(string Message, Exception? InnerException);

public record PackNotFoundError(string NotFoundMessage) : PackError(NotFoundMessage, null) ;

public record PackExceptionError(Exception InnerException) : PackError(InnerException.Message, InnerException);
