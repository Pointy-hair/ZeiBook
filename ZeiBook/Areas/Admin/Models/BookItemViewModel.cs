using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ZeiBook.Models;

namespace ZeiBook.Areas.Admin.Models
{
    public class BookItemViewModel
    {
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }
        public int? AuthorId { get; set; }
        public DateTime UploadTime { get; set; }
        public int CategoryId { get; set; }
        public Gender Gender { get; set; }
        public String Path { get; set; }
        public String Description { get; set; }
        public int DownloadTime { get; set; } = 0;
        public long FileLength { get; set; } = 0;//clean
        public string CoverPath { get; set; }
        public String Author { get; set; }

        public BookItemViewModel() { }

        public BookItemViewModel(Book book)
        {
            this.Id = book.Id;
            this.Name = book.Name;
            this.AuthorId = book.AuthorId;
            this.UploadTime = book.UploadTime;
            this.Gender = book.Gender;
            this.Path = book.Path;
            this.CategoryId = book.CategoryId;
            this.Description = book.Description;
            this.DownloadTime = book.DownloadTime;
            this.FileLength = book.FileLength;
            this.CoverPath = book.CoverPath;
        }

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
