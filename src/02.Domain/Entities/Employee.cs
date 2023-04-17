using CRUD.ManagementUser.Domain.Abstracts;

namespace CRUD.ManagementUser.Domain.Entities;

public class Employee : AuditableEntity
{
    public string Number { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string Address { get; set; } = default!;
    //public string Position { get; set; } = default!;
    //public string Status { get; set; } = default!;
    //public string ApplicationId { get; set; } = default!;
}
