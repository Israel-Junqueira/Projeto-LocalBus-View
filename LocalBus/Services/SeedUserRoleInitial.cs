using LocalBus.Models;
using Microsoft.AspNetCore.Identity;

namespace LocalBus.Services
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        // injeção das instancias
        private readonly UserManager<MyUser> _UserManager;
        private readonly RoleManager<IdentityRole> _RoleManager;

        public SeedUserRoleInitial(UserManager<MyUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _UserManager = userManager;
            _RoleManager = roleManager;
        }

        public void SeedRoles()
        {
            if (!_RoleManager.RoleExistsAsync("Member").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Member";
                role.NormalizedName = " MEMBER";
                IdentityResult roleResult = _RoleManager.CreateAsync(role).Result;

    
            }
            if (!_RoleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                role.NormalizedName = " Admin";
                IdentityResult roleResult = _RoleManager.CreateAsync(role).Result;


            }
        }

        public void SeedUsers()
        {
            if (_UserManager.FindByEmailAsync("Admin").Result == null)
            {
                MyUser user = new MyUser();
                user.UserName = "Admin";
                user.Email = "admin@localhost";
                user.NormalizedEmail = "ADMIN@LOCALHOST";
                user.NormalizedEmail = "ADMIN@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();
           

                IdentityResult result = _UserManager.CreateAsync(user, "Admin123@").Result;

                if (result.Succeeded)
                {
                    _UserManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }

            if (_UserManager.FindByEmailAsync("Member").Result == null)
            {
                MyUser user = new MyUser();
                user.UserName = "Member";
                user.Email = "Member@localhost";
                user.NormalizedEmail = "MEMBER@LOCALHOST";
                user.NormalizedEmail = "MEMBER@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();


                IdentityResult result = _UserManager.CreateAsync(user, "Member123@").Result;

                if (result.Succeeded)
                {
                    _UserManager.AddToRoleAsync(user, "Member").Wait();
                }
            }
        }
    }
}
