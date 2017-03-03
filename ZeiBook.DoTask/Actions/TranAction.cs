using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using ZeiBook.Commons;
using ZeiBook.Data;
using ZeiBook.DoTask.Models;
using ZeiBook.Models;

namespace ZeiBook.DoTask.Actions
{
    public class TranAction
    {
        private string _webRootPath = @"F:\work\code\2017\4\csharp\ZeiBook\ZeiBook\wwwroot";
        private List<string> _boyClass = new List<string> { "官职商战", "东方传奇", "间谍暗战", "探险揭秘",
            "特种军旅", "魔法校园", "未来幻想","虚拟网游","诗歌文集","异术超能","奇幻修真","乡土布衣",
            "奇幻魔法","江湖武侠","王室贵族","历史传记","穿越架空","竞技体育","灵异鬼怪","王朝争霸"};

        /*
官职商战 东方传奇 间谍暗战 探险揭秘
特种军旅 魔法校园 百合之恋 未来幻想
虚拟网游 诗歌文集 唯美言情 异术超能
奇幻修真 乡土布衣 魔幻女强 奇幻魔法
江湖武侠 王室贵族 历史传记 穿越架空
竞技体育 灵异鬼怪 都市婚姻 王朝争霸
同人美文
*/
        private ApplicationDbContext _context;

        public TranAction(ApplicationDbContext context)
        {
            _context = context;
        }

        public Book GetBook(BookData item)
        {
            var bookItem = new Book
            {
                Name = item.Title,
                Author = item.AuthorName,
                UploadTime = item.UploadTime
            };
            bookItem.CoverPath = GetCoverPath(item.Title);
            var categoryItem = _context.Categories.SingleOrDefault(t => t.Name == item.Class);
            if (categoryItem==null)
            {
                categoryItem = new Category { Name = item.Class };
                _context.Categories.Add(categoryItem);
                _context.SaveChanges();
            }
            bookItem.CategoryId = categoryItem.Id;
            bookItem.Gender = _boyClass.Any(t => t == item.Class)?Gender.Boy:Gender.Girl;
            return bookItem;
        }

        private string GetCoverPath(string name)
        {
            //保存文件夹
            string fileDir = DateTime.Now.ToString(@"\\bookCover\\yy\\MM\\dd");
            Directory.CreateDirectory(_webRootPath + fileDir);
            //保存文件名
            var title = Guid.NewGuid().ToString("N");
            string fileName = string.Format("{0}\\{1}.png", fileDir, title);
            var coverImage = BookCoverCommon.Create(name);

            coverImage.Save(_webRootPath + fileName, ImageFormat.Png);
            return fileName;
        }

    }
}
