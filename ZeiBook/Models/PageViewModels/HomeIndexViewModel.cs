using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZeiBook.Models.PageViewModels
{
    public class HomeIndexViewModel
    {
        public List<Book> BoyBooks { get; set; }
        public List<Book> GirlBooks { get; set; }
        public List<Book> TopDownloadBooks { get; set; }
    }
}
