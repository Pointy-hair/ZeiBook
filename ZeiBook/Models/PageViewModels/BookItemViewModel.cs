using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZeiBook.Models.PageViewModels
{
    public class BookItemViewModel:Book
    {
        public String Author { get; set; }

        //[Bind("Id,Name,Author,CategoryId,Description,Gender,UploadTime")
        public Book GetBook()
        {
            return new Book
            {
                Id = this.Id,
                Name = this.Name,
                CategoryId = this.CategoryId,
                Description = this.Description,
                Gender = this.Gender,
                UploadTime = this.UploadTime
            };
        }
    }
}
