using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeiBook.Models.Utils;

namespace ZeiBook.Models.PageViewModels
{
    public class SearchViewModel
    {
        public string Keyword { get; set; }
        public List<Book> Books { get; set; }
        public RoutePageOption PageOption { get; set; }
    }
}
