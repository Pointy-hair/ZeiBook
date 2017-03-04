using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeiBook.Data;
using ZeiBook.Models;
using ZeiBook.Models.PageViewModels;
using ZeiBook.Models.Utils;

namespace ZeiBook.Actions.Book
{
    public class GenderIndexAction
    {
        private ApplicationDbContext _context;

        public GenderIndexAction(ApplicationDbContext context)
        {
            _context = context;
        }

        private GenderIndexViewModel GetViewModel(Gender gender, PageOption option)
        {
            var skipNum = (option.PageNum - 1) * option.PageSize;
            var list = _context.Books.Include(t=>t.Writer).Where(b => b.Gender == gender).OrderByDescending(b => b.UploadTime).Take(option.PageSize).ToList();

            var model = new GenderIndexViewModel
            {
                Gender = gender,
                Books = list,
                PageOption = option
            };
            return model;
        }

        private PageOption GetPageOption(Gender gender, int pageNum, int pageSize)
        {
            var pageCount = (int)Math.Ceiling(_context.Books.Count(b => b.Gender==gender) / (double)pageSize);
            return new PageOption
            {
                PageCount = pageCount,
                PageNum = pageNum,
                PageSize = pageSize
            };
        }

        public GenderIndexViewModel GetViewModel(Gender gender, int pageNum, int pageSize=15)
        {
            var po = GetPageOption(gender, pageNum, pageSize);
            if (!po.Valid()) return null;
            return GetViewModel(gender, po);
        }
    }
}
