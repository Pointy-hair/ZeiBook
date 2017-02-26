using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZeiBook.Models
{
    public class BookDayInfo
    {
        public int Id { get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public DateTime Day { get; set; }
        public int Count { get; set; }

        public Book Book { get; set; }
    }
}
