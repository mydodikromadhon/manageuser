﻿using CRUD.ManagementUser.Application.Common.Constants;

namespace CRUD.ManagementUser.Application.Common.Exceptions;

public class AlreadyExistsException : Exception
{
    public AlreadyExistsException()
        : base()
    {
    }

    public AlreadyExistsException(string message)
        : base(message)
    {
    }

    public AlreadyExistsException(string entityName, object key)
        : base($"{entityName} with {CommonDisplayTextFor.Id} [{key}] already exists.")
    {
    }

    public AlreadyExistsException(string entityName, string entityField, object value)
        : base($"{entityName} with {entityField}: {value} already exists.")
    {
    }
}
