using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ZeiBook.Data;
using ZeiBook.Models;

namespace ZeiBook.Areas.Admin.Actions.Roles
{
    public class RoleConfigAction
    {
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext _context;

        public RoleConfigAction(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }

        public async Task Config(ClaimsPrincipal userClaims)
        {
            var user = await _userManager.GetUserAsync(userClaims);
            var role = await _roleManager.FindByNameAsync("admin");
            if (role==null)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole { Name = "admin" });
                if (!result.Succeeded)
                {
                    return;
                }
            }
            if (!_context.UserRoles.Any(t => t.RoleId == role.Id)) { 
                await _userManager.AddToRoleAsync(user, "admin");
            }
        }

    }
}
