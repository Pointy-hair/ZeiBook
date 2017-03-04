using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZeiBook.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }
        [ForeignKey("Writer")]
        public int? AuthorId { get; set; }
        public DateTime UploadTime { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Gender Gender { get; set; }
        public String Path { get; set; }
        public String Description { get; set; }
        public int DownloadTime { get; set; } = 0;
        public long FileLength { get; set; } = 0;//clean
        public string CoverPath { get; set; }

        public Category Category { get; set; }
        public Author Writer { get; set; }
    }

    public enum Gender
    {
        Boy,Girl
    }
}
