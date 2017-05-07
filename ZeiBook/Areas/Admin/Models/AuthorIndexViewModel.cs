using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeiBook.Models;
using ZeiBook.Models.Utils;

namespace ZeiBook.Areas.Admin.Models
{
    public class AuthorIndexViewModel
    {
        public List<Author> Authors { get; set; }
        public PageOption PageOption { get; set; }
    }
}
