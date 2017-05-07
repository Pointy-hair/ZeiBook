using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeiBook.Data;
using ZeiBook.Models.PageViewModels;
using ZeiBook.Models.Utils;

namespace ZeiBook.Actions.Search
{
    public class SearchAction
    {
        private ApplicationDbContext _context;

        public SearchAction(ApplicationDbContext context)
        {
            this._context = context;
        }

        public SearchViewModel GetViewModel(string keyword, int pageNum, int pageSize = 15)
        {
            var coll = from b in _context.Books
                       join a in _context.Authors on b.AuthorId equals a.Id
                       where a.Name.Contains(keyword) || b.Name.Contains(keyword)
                       select b;

            var pageCount = (int)Math.Ceiling(coll.Count() / (double)pageSize);
            var po = new RoutePageOption { PageCount = pageCount, PageSize = pageSize, PageNum = pageNum };
            if (!po.Valid()) return null;

            po.Routes = new RouteValueDictionary();
            po.Routes.Add("keyword", keyword);

            var skipNum = (pageNum - 1) * pageSize;
            var list = coll.Include(t=>t.Writer).Include(t=>t.Category).OrderBy(b => b.UploadTime).Skip(skipNum).Take(pageSize).ToList();
            return new SearchViewModel
            {
                Books = list,
                Keyword = keyword,
                PageOption = po
            };
        }
    }
}
