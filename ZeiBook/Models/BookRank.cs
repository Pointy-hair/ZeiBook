using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZeiBook.Models
{
    public class BookRank
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan Interval { get; set; }
    }

    public class BookRankResultItem
    {
        [ForeignKey("BookRank")]
        public int BookRankId { get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public int Position { get; set; }

        public BookRank BookRank { get; set; }
        public Book Book { get; set; }
    }
}
