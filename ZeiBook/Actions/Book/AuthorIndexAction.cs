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
    public class AuthorIndexAction
    {
        private ApplicationDbContext _context;

        public AuthorIndexAction(ApplicationDbContext context)
        {
            _context = context;
        }

        public AuthorIndexViewModel GetViewModel(string name, int pageNum, int pageSize=15)
        {
            var author = _context.Authors.SingleOrDefault(t => t.Name == name);
            if (author==null)
            {
                return null;
            }
            var coll = _context.Books.Where(t => t.AuthorId == author.Id);
            var pageCount = (int)Math.Ceiling(coll.Count() / (double)pageSize);
            var po = new PageOption
            {
                PageCount = pageCount,
                PageNum = pageNum,
                PageSize = pageSize
            };
            if (!po.Valid()) return null;

            var skipNum = (pageNum - 1) * pageSize;
            var list = coll.Include(t=>t.Category).OrderByDescending(b => b.UploadTime).Take(pageSize).ToList();

            var model = new AuthorIndexViewModel
            {
                Author = author,
                Books = list,
                PageOption = po
            };
            return model;
        }
    }
}
