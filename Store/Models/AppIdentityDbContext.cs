using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TestStore.Models
{
    public class AppIdentityDbContext:IdentityDbContext<IdentityUser>
    {
        public AppIdentityDbContext(DbContextOptions options):base(options) { }
    }
}
