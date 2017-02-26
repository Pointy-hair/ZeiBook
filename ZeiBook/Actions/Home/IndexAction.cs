using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeiBook.Data;
using ZeiBook.Models;
using ZeiBook.Models.PageViewModels;
using Microsoft.EntityFrameworkCore;

namespace ZeiBook.Actions.Home
{
    public class IndexAction
    {
        private ApplicationDbContext _context;

        public IndexAction(ApplicationDbContext context)
        {
            this._context = context;
        }

        public HomeIndexViewModel GetViewModel()
        {
            var item = new HomeIndexViewModel();
            //Boy book
            item.BoyBooks = _context.Books.Include(b=>b.Category).Where(b => b.Gender == Gender.Boy).OrderByDescending(b => b.UploadTime).Take(10).ToList();
            //Girl book
            item.GirlBooks = _context.Books.Include(b => b.Category).Where(b => b.Gender == Gender.Girl).OrderByDescending(b => b.UploadTime).Take(10).ToList();
            //点击排行
            item.TopDownloadBooks = _context.Books.OrderByDescending(b => b.DownloadTime).Take(10).ToList();
            return item;
        }
    }
}
