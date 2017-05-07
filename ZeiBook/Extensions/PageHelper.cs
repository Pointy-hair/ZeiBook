using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeiBook.Models.Utils;

namespace ZeiBook.Extensions
{
    public static class PageHelper
    {
        private static int _length=10;

        public static PageRenderItem ParsePageOption(this IHtmlHelper helper,PageOption po)
        {
            var startNum = (po.PageNum - 1) / _length * _length + 1;
            var endNum = ((po.PageNum - 1) / _length+1) * _length;
            return new PageRenderItem
            {
                RenderStart = startNum,
                RenderEnd = endNum>po.PageCount?po.PageCount:endNum,
            };
        }

    }

    public class PageRenderItem
    {
        public int RenderStart { get; set; }
        public int RenderEnd { get; set; }
    }
}
