using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ZeiBook.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ZeiBook.Areas.Admin.Actions.Roles;
using ZeiBook.Areas.Admin.Models;

namespace ZeiBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="admin")]
    public class RolesController : Controller
    {
        // GET: Roles
        public async Task<ActionResult> Index([FromServices]RoleIndexAction action)
        {
            var model = await action.GetViewModel(User);
            return View(model);
        }

        // GET: Roles/Details/5
        public async Task<ActionResult> Details(string id, [FromServices]RoleManager<IdentityRole> roleManager)
        {
            var model = await roleManager.FindByIdAsync(id);
            return View(model);
        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RoleAddViewModel roleModel,[FromServices]RoleManager<IdentityRole> roleManager)
        {
            try
            {
                var role = new IdentityRole { Name = roleModel.Name };
                var result = await roleManager.CreateAsync(role);
                if (!result.Succeeded)
                {
                    return View();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Roles/Edit/5
        public async Task<ActionResult> Edit(string id, [FromServices]RoleManager<IdentityRole> roleManager)
        {
            var item = await roleManager.FindByIdAsync(id);
            return View(item);
        }

        // POST: Roles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, RoleAddViewModel roleModel, [FromServices]RoleManager<IdentityRole> roleManager)
        {
            try
            {
                var item = await roleManager.FindByIdAsync(id);
                await roleManager.SetRoleNameAsync(item, roleModel.Name);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Roles/Delete/5
        public async Task<ActionResult> Delete(string id, [FromServices]RoleManager<IdentityRole> roleManager)
        {
            var item = await roleManager.FindByIdAsync(id);
            return View(item);
        }

        // POST: Roles/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeletePost(string id, [FromServices]RoleManager<IdentityRole> roleManager)
        {
            try
            {
                var item = await roleManager.FindByIdAsync(id);
                await roleManager.DeleteAsync(item);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Roles/Config
        [AllowAnonymous]
        public async Task<ActionResult> Config([FromServices]RoleConfigAction action)
        {
            if (User == null)
            {
                return NotFound();
            }

            await action.Config(User);
            
            return RedirectToAction("Index");
        }
    }
}