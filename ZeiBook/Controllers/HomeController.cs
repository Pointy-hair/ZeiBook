using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZeiBook.Data;
using ZeiBook.Actions.Home;

namespace ZeiBook.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index([FromServices]IndexAction action)
        {
            return View(action.GetViewModel());
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
