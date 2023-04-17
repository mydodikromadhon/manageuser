using CRUD.ManagementUser.Application.Common.Extensions;

namespace CRUD.ManagementUser.Application.Employees.Constants;

public static class DisplayTextFor
{
    public const string Employees = nameof(Employees);
    public const string Employee = nameof(Employee);

    public static readonly string Number = nameof(Number);
    public static readonly string FullName = nameof(FullName).SplitWords();
}
