using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZeiBook.Models.Utils
{
    public class PageOption
    {
        public int PageNum { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }

        public bool Valid()
        {
            return PageNum > 0 && PageNum <= PageCount||(PageCount==0&&PageNum==1);
        }
    }
}
