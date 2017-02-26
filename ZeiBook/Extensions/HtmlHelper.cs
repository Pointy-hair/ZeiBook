using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZeiBook.Extensions
{
    public static class HtmlHelper
    {
        private static int _kbNum = 1024;
        private static int _mbNum = _kbNum * 1024;
        private static int _gbNum = _mbNum * 1024;

        public static HtmlString DisplayShortDate(this IHtmlHelper helper,DateTime date)
        {
            return new HtmlString(date.ToString("MM-dd"));
        }

        public static HtmlString DisplayFileLength(this IHtmlHelper helper, long fileLength)
        {
            int targetNum=1;
            String targetStr = "B";
            if (fileLength>=_gbNum)
            {
                targetNum = _mbNum;
                targetStr = "GB";
            }else if (fileLength >= _mbNum)
            {
                targetNum = _mbNum;
                targetStr = "MB";
            }else if (fileLength >= _kbNum)
            {
                targetNum = _mbNum;
                targetStr = "MB";
            }

            //var item = Math.Round(fileLength / (double)targetNum, 2);
            var num = fileLength / (double)targetNum;
            return new HtmlString(string.Format("{0:F}{1}",num,targetStr));
        }

        
    }
}
