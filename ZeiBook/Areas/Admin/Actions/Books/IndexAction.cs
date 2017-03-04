using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            if (bookName != null)
            {
                coll = _context.Books.Where(t => t.Name.Contains(bookName));
            }else
            {
                coll = _context.Books;
            }
            var pageCount = (int)Math.Ceiling(coll.Count() / (double)pageSize);
            var po = new SearchPageOption
            {
                PageSize = pageSize,
                PageCount = pageCount,
                PageNum = pageNum
            };
            if (!po.Valid()) return null;
            var list = await coll.OrderByDescending(t => t.UploadTime)
                .Skip((pageNum - 1) * pageSize).Take(pageSize).ToListAsync();

            var routes = new RouteValueDictionary();
            routes.Add("bookName", bookName);
            po.Routes = routes;
            po.ActionName = "Index";
            po.ControllerName = "Books";

            return new BookIndexViewModel
            {
                Books = list,
                PageOption = po
            };
        }
    }
}
