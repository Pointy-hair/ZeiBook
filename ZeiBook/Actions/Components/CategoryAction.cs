using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeiBook.Data;
using ZeiBook.Models;

namespace ZeiBook.Actions.Components
{
    public class CategoryAction
    {
        private ApplicationDbContext _context;

        public CategoryAction(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Category> GetList()
        {
            return _context.Categories.ToList();
        }
    }
}
