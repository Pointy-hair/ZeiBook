using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeiBook.Data;
using ZeiBook.Models.PageViewModels;
using ZeiBook.Models.Utils;

namespace ZeiBook.Actions.Book
{
    public class BookIndexAction
    {
        private readonly ApplicationDbContext _context;

        public BookIndexAction(ApplicationDbContext context)
        {
            _context = context;
        }

        public BookIndexViewModel GetViewModel(int pageNum,int pageSize=15)
        {
            var po = new PageOption { PageNum = pageNum, PageSize = pageSize };
            po.PageCount = (int)Math.Ceiling(_context.Books.Count()/(double)pageSize);
            if (!po.Valid()) return null;
            var skipNum = (pageNum - 1) * pageSize;
            var list = _context.Books.Include(t=>t.Writer).OrderByDescending(b=>b.UploadTime).Skip(skipNum).Take(pageSize).ToList();
            var model = new BookIndexViewModel
            {
                PageOption = po,
                Books = list
            };
            return model;
        }
    }
}
