using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Infra.Data.Identity;

public class SeedUserRoleInitial : ISeedUserRoleInitial
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public SeedUserRoleInitial(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public void SeedUsers()
    {
        if (_userManager.FindByEmailAsync("user@localhost").Result == null)
        {
            ApplicationUser user = new();
            user.UserName = "user@localhost";
            user.Email = "user@localhost";
            user.NormalizedUserName = "USER@LOCALHOST";
            user.NormalizedEmail = "USER@LOCALHOST";
            user.EmailConfirmed = true;
            user.LockoutEnabled = false;
            user.SecurityStamp = Guid.NewGuid().ToString();

            IdentityResult result = _userManager.CreateAsync(user, "Pass#123").Result;

            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(user, "User").Wait();
            }
        }

        if (_userManager.FindByEmailAsync("admin@localhost").Result == null)
        {
            ApplicationUser user = new()
            {
                UserName = "admin@localhost",
                Email = "admin@localhost",
                NormalizedUserName = "ADMIN@LOCALHOST",
                NormalizedEmail = "ADMIN@LOCALHOST",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            IdentityResult result = _userManager.CreateAsync(user, "Pass#123").Result;

            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(user, "Admin").Wait();
            }
        }
    }

    public void SeedRoles()
    {
        if (!_roleManager.RoleExistsAsync("User").Result)
        {
            IdentityRole role = new()
            {
                Name = "User",
                NormalizedName = "USER"
            };

            IdentityResult result = _roleManager.CreateAsync(role).Result;
        }

        if (!_roleManager.RoleExistsAsync("Admin").Result)
        {
            IdentityRole role = new()
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            };

            IdentityResult result = _roleManager.CreateAsync(role).Result;
        }
    }   
}
