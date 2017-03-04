using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZeiBook.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using ZeiBook.Actions.Book;
using ZeiBook.Models.Utils;
using ZeiBook.Models;
using System.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZeiBook.Controllers
{
    public class BookController : Controller
    {
        private ApplicationDbContext _context;

        public BookController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("/Book", Order = 1)]
        [Route("/Book/p{pageNum}", Order = 0)]
        public IActionResult Index(int? pageNum, [FromServices]BookIndexAction action)
        {
            var model = action.GetViewModel(pageNum ?? 1);
            if (model == null) return NotFound();
            return View(model);
        }

        public IActionResult Item(int id)
        {
            var item = _context.Books.Include(t => t.Category).Include(t => t.Writer).SingleOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        public IActionResult Down(int id, [FromServices]IHostingEnvironment env, [FromServices]DownloadAction action)
        {
            var item = _context.Books.SingleOrDefault(t => t.Id == id);
            if (item != null && item.Path != null)
            {
                var fileName = env.WebRootPath + item.Path;
                if (System.IO.File.Exists(fileName))
                {
                    action.GainDownloadCount(id);
                    return File(new FileStream(fileName, FileMode.Open), "application/octet-stream", item.Name + ".txt");
                }
            }
            return NotFound();
        }

        [Route("/c/{id}", Order = 1)]
        [Route("/c/{id}/p{pageNum}", Order = 0)]
        public IActionResult CategoryIndex(int id, int? pageNum, [FromServices]CategoryIndexAction action)
        {
            if (!action.Exist(id))
            {
                return NotFound();
            }
            var model = action.GetViewModel(id, pageNum ?? 1);
            if (model == null) return NotFound();
            return View(model);
        }

        [Route("/g/{gender}", Order = 1)]
        [Route("/g/{gender}/p{pageNum}", Order = 0)]
        public IActionResult GenderIndex(Gender? gender, int? pageNum, [FromServices]GenderIndexAction action)
        {
            if (gender == null)
            {
                return NotFound();
            }
            var model = action.GetViewModel((Gender)gender, pageNum ?? 1);
            if (model == null) return NotFound();
            return View(model);
        }
    }
}
