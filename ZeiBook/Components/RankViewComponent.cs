using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeiBook.Actions.Components;
using ZeiBook.Data;
using ZeiBook.Models;

namespace ZeiBook.Components
{
    public class RankViewComponent:ViewComponent
    {
        private ApplicationDbContext _context;

        public RankViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int? categoryId,Gender? gender)
        {
            List<Book> list = null;
            if (categoryId != null)
            {
                list = _context.Books
                    .Where(t=>t.CategoryId== (int)categoryId)
                    .OrderByDescending(t => t.DownloadTime).Take(10).ToList();
            }else if (gender!=null)
            {
                list = _context.Books
                    .Where(t => t.Gender == gender)
                    .OrderByDescending(t => t.DownloadTime).Take(10).ToList();
            }
            else
            {
                list = _context.Books
                    .OrderByDescending(t => t.DownloadTime).Take(10).ToList();
            }
            return View(list);
        }
    }
}
