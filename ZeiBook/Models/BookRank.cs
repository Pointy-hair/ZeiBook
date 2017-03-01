using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZeiBook.Models
{
    public class BookRank
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public long DurationTicks { get; set; }
        [NotMapped]
        [Display(Name = "回溯时间间隔")]
        public TimeSpan Interval
        {
            get
            {
                return new TimeSpan(DurationTicks);
            }
            set { DurationTicks = value.Ticks; }
        }
        public String CornValue { get; set; }
        public bool EnableTask { get; set; }
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
