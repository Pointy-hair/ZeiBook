using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ZeiBook.Areas.Admin.Models;
using ZeiBook.Data;
using ZeiBook.Models;
using ZeiBook.Models.Utils;

namespace ZeiBook.Areas.Admin.Actions.Books
{
    public class IndexAction
    {
        private ApplicationDbContext _context;

        public IndexAction(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BookIndexViewModel> GetViewModelAsync(int pageNum, string bookName, int pageSize = 50)
        {
            IQueryable<Book> coll = null;
            Expression<Func<Book, bool>> p = null;
            if (bookName != null)
            {
                p = t => t.Name.Contains(bookName);
            }else
            {
                p = t=>true;
            }
            coll = _context.Books.Where(p);
            var pageCount = (int)Math.Ceiling(coll.Count() / (double)pageSize);
            var po = new RoutePageOption
            {
                PageSize = pageSize,
                PageCount = pageCount,
                PageNum = pageNum
            };
            if (!po.Valid()) return null;
            var list = await coll.Include(t=>t.Writer).Include(t=>t.Category)
                .OrderByDescending(t => t.UploadTime)
                .Skip((pageNum - 1) * pageSize).Take(pageSize).ToListAsync();

            po.Routes = new RouteValueDictionary();
            po.Routes.Add("pagenum", pageNum);
            po.Routes.Add("bookName", bookName);

            return new BookIndexViewModel
            {
                Books = list,
                PageOption = po
            };
        }
    }
}
