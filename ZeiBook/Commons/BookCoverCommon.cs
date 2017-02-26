using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace ZeiBook.Commons
{
    public class BookCoverCommon
    {
        private static string fontStr = "微软雅黑";
        private static int imgHeight = 256;
        private static int imgWidth = 192;

        public static Bitmap Create(string title)
        {
            Bitmap bmp = new Bitmap(imgWidth, imgHeight);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.LightBlue);
            int chWidth = imgWidth / (CountStr(title)+1);
            int extraLength = chWidth / 2;
            Font ft = new Font(fontStr, chWidth*3/(float)4);
            g.DrawString(title, ft, new SolidBrush(Color.Black), extraLength, imgHeight / 2 - chWidth);
            g.Dispose();
            return bmp;
        }

        public static int CountStr(string str)
        {
            var count = 0;
            foreach (var item in str)
            {
                count += item < 'z' ? 1 : 2;
            }
            return (int)Math.Ceiling(count / (double)2);
        }
    }
}
