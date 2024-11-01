using Core.CrossCuttingConcerns.Exceptions.Types;

namespace Core.CrossCuttingConcerns.Exceptions.Handlers;

public abstract class ExceptionHandler
{
    public Task HandleExceptionAsync(Exception exception) =>
        exception switch
        {
            BusinessException businessException => HandleException(businessException),
            ValidationException validationException => HandleException(validationException),
            AuthorizationException authorizationException => HandleException(authorizationException),
            BanException banException => HandleException(banException),
            NotFoundException notFoundException => HandleException(notFoundException),
            _ => HandleException(exception)
        };

    protected abstract Task HandleException(BusinessException businessException);
    protected abstract Task HandleException(ValidationException validationException);
    protected abstract Task HandleException(AuthorizationException authorizationException);
    protected abstract Task HandleException(BanException banException);
    protected abstract Task HandleException(NotFoundException notFoundException);
    protected abstract Task HandleException(Exception exception);
}
