using CRUD.ManagementUser.Application.Services.DateAndTime;

namespace CRUD.ManagementUser.Infrastructure.DateAndTime;

public class DateAndTimeService : IDateAndTimeService
{
    public DateTimeOffset Now => DateTimeOffset.Now;
}
