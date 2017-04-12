using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZeiBook.Areas.Admin.Models
{
    public class RoleIndexViewModel
    {
        public IList<string> UserRoles { get; set; }
        public List<IdentityRole> Roles { get; set; }
    }

    public class RoleAddViewModel
    {
        public string Name { get; set; }
    }
}
