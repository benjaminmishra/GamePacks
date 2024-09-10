namespace GamePacks.Service.Models.Errors;

public record PackError(string Message, Exception? InnerException);

public record PackValidationError(string ValidationErrorMessage) : PackError(ValidationErrorMessage, null);

public record PackNotFoundError(string NotFoundMessage) : PackError(NotFoundMessage, null);

public record PackExceptionError(Exception InnerException) : PackError(InnerException.Message, InnerException);
