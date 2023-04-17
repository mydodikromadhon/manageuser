﻿namespace CRUD.ManagementUser.Domain.Interfaces;

public interface IModifiable
{
    DateTimeOffset? Modified { get; set; }
    string? ModifiedBy { get; set; }
}
