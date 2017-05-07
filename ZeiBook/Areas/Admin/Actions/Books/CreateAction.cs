using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ZeiBook.Commons;

namespace ZeiBook.Areas.Admin.Actions.Books
{
    public class CreateAction
    {
        private IHostingEnvironment _env;

        public CreateAction(IHostingEnvironment env)
        {
            _env = env;
        }

        /// <summary>
        /// 保存上传文件
        /// </summary>
        /// <param name="file">IFormFile</param>
        /// <param name="Title"></param>
        /// <returns>文件路径</returns>
        public string SaveFile(IFormFile file)
        {
            if (file == null) return null;

            var ext = Path.GetExtension(file.FileName).ToLower();
            if (ext!=".txt")
            {
                return null;
            }
            //保存文件夹
            string fileDir = DateTime.Now.ToString(@"\\book\\yy\\MM\\dd");
            Directory.CreateDirectory(_env.WebRootPath + fileDir);
            //保存文件名
            var title = Guid.NewGuid().ToString("N");
            string fileName = string.Format("{0}\\{1}{2}", fileDir, title, ext);
            var stream1 = new FileStream(_env.WebRootPath + fileName, FileMode.Create);
            //TODO: 上传费时
            file.CopyTo(stream1);
            stream1.Flush();
            stream1.Dispose();
            return fileName;
        }

        public string GetCoverPath(string name)
        {
            //保存文件夹
            string fileDir = DateTime.Now.ToString(@"\\bookCover\\yy\\MM\\dd");
            Directory.CreateDirectory(_env.WebRootPath + fileDir);
            //保存文件名
            var title = Guid.NewGuid().ToString("N");
            string fileName = string.Format("{0}\\{1}.png", fileDir, title);
            var coverImage = BookCoverCommon.Create(name);

            coverImage.Save(_env.WebRootPath+fileName, ImageFormat.Png);
            return fileName;
        }

        public string GetHtmlDescription(string description)
        {
            return description!=null?description.Replace("\n", "<br>"):null;
        }
    }
}
