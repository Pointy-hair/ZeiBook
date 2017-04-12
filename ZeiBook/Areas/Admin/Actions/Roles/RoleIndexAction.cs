using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeiBook.Areas.Admin.Models;
using ZeiBook.Models;
using System.Security.Claims;

namespace ZeiBook.Areas.Admin.Actions.Roles
{
    public class RoleIndexAction
    {
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;

        public RoleIndexAction(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<RoleIndexViewModel> GetViewModel(ClaimsPrincipal userClaims)
        {
            var model = new RoleIndexViewModel();
            var user = await _userManager.GetUserAsync(userClaims);
            model.UserRoles = await _userManager.GetRolesAsync(user);
            model.Roles =  _roleManager.Roles.ToList();
            return model;
        }

    }
}
