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

        public SearchViewModel GetViewModel(string keyword, int pageNum, int pageSize=15)
        {
            var coll = _context.Books.Where(
                b => b.Name.Contains(keyword) ||
                b.Author.Contains(keyword));

            var pageCount = (int)Math.Ceiling(coll.Count() / (double)pageSize);
            var po = new PageOption { PageCount = pageCount, PageSize = pageSize, PageNum = pageNum };
            if (!po.Valid()) return null;

            var skipNum = (pageNum - 1) * pageSize;
            var list = coll.OrderBy(b => b.UploadTime).Skip(skipNum).Take(pageSize).ToList();
            return new SearchViewModel
            {
                Books = list,
                Keyword = keyword
            };
        }
    }
}
