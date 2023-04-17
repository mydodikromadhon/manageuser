namespace CRUD.ManagementUser.Application.Common.Exceptions;

public class ForbiddenAccessException : Exception
{
    public ForbiddenAccessException()
        : base()
    {
    }

    public ForbiddenAccessException(string message)
        : base(message)
    {
    }
}
