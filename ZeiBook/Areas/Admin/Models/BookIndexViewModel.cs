using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeiBook.Models;
using ZeiBook.Models.Utils;

namespace ZeiBook.Areas.Admin.Models
{
    public class BookIndexViewModel
    {
        public RoutePageOption PageOption { get; set; }
        public List<Book> Books { get; set; }
    }
}
