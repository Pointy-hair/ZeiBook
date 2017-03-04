using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeiBook.Areas.Admin.Models;
using ZeiBook.Data;
using ZeiBook.Models;
using ZeiBook.Models.PageViewModels;
using ZeiBook.Models.Utils;

namespace ZeiBook.Areas.Admin.Actions.Authors
{
    public class IndexAction
    {
        private ApplicationDbContext _context;

        public IndexAction(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AuthorIndexViewModel> GetViewModelAsync(int pageNum,string authorName, int pageSize = 50)
        {
            IQueryable<Author> coll = null;
            if (authorName != null)
            {
                coll = _context.Authors.Where(t => t.Name.Contains(authorName));
            }
            else
            {
                coll = _context.Authors;
            }
            var pageCount = (int)Math.Ceiling(coll.Count() / (double)pageSize);
            var po = new SearchPageOption
            {
                PageSize = pageSize,
                PageCount = pageCount,
                PageNum = pageNum,
                ActionName = "Index",
                ControllerName = "Authors",
            };
            if (!po.Valid()) return null;
            var list = await coll.OrderByDescending(t => t.Name)
                .Skip((pageNum - 1) * pageSize).Take(pageSize).ToListAsync();

            po.Routes = new RouteValueDictionary();
            po.Routes.Add("pageNum", pageNum);
            po.Routes.Add("authorName", authorName);
            return new AuthorIndexViewModel
            {
                Authors = list,
                PageOption = po
            };
        }
    }
}
