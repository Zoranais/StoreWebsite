using Microsoft.AspNetCore.Identity;

namespace TestStore.Models
{
    public class IdentitySeedData
    {
        private const string adminUser = "admin";
        private const string adminPassword = "Admin123$";

        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                UserManager<IdentityUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                IdentityUser user=await userManager.FindByIdAsync(adminUser);
                if (user == null)
                {
                    user=new IdentityUser(adminUser);
                    await userManager.CreateAsync(user,adminPassword);
                }
            }
        }
    }
}
