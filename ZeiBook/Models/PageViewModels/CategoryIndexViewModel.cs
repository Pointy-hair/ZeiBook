using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeiBook.Models.Utils;

namespace ZeiBook.Models.PageViewModels
{
    public class CategoryIndexViewModel
    {
        public Category Category { get; set; }
        public List<Book> Books { get; set; }
        public PageOption PageOption { get; set; }
    }
}
