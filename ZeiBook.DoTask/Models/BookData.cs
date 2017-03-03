using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ZeiBook.DoTask.Models
{
    public class BookData
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public String Title { get; set; }
        public String AuthorName { get; set; }
        public String InfoLink { get; set; }
        public String DownLink { get; set; }
        public String Class { get; set; }
        public String Size { get; set; }
        public DateTime UploadTime { get; set; }
        public String Description { get; set; }
        public bool IsDownload { get; set; }
    }
}
