using Microsoft.AspNet.Identity.EntityFramework;

namespace CRUD.ManagementUser.Domain.Entities
{
    //public class ApplicationUser : IdentityUser
    public class ApplicationUser
    {
        public string FullName { get; set; } = default!;
        public string Position { get; set; } = default!;
        public string Status { get; set; } = default!;
    }
}
