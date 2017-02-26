using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeiBook.Data;
using ZeiBook.Models.PageViewModels;
using ZeiBook.Models.Utils;

namespace ZeiBook.Actions.Book
{
    public class CategoryIndexAction
    {
        private ApplicationDbContext _context;

        public CategoryIndexAction(ApplicationDbContext context)
        {
            _context = context;
        }

        private CategoryIndexViewModel GetViewModel(int cateId, PageOption option)
        {
            var category = _context.Categories.SingleOrDefault(c => c.Id == cateId);
            
            var skipNum = (option.PageNum - 1) * option.PageSize;
            var list = _context.Books.Where(b=>b.CategoryId==cateId).OrderByDescending(t => t.UploadTime).Skip(skipNum).Take(option.PageSize).ToList();
            var model = new CategoryIndexViewModel()
            {
                Category = category,
                Books = list,
                PageOption = option
            };
            return model;
        }

        private PageOption GetPageOption(int cateId, int pageNum, int pageSize=15)
        {
            var pageCount = (int)Math.Ceiling(_context.Books.Count(b => b.CategoryId == cateId)/(double)pageSize);
            return new PageOption
            {
                PageCount = pageCount,
                PageNum = pageNum,
                PageSize = pageSize
            };
        }

        public bool Exist(int id)
        {
            return _context.Categories.Any(c => c.Id == id);
        }

        public CategoryIndexViewModel GetViewModel(int id, int pageNum, int pageSize = 15)
        {
            var po = GetPageOption(id, pageNum, pageSize);
            if (!po.Valid()) return null;
            return GetViewModel(id,po);
        }
    }
}
