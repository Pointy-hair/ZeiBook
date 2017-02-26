using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZeiBook.Models.Utils
{
    public class BookRankModel
    {
        public BookRank Rank { get; set; }
        public List<BookRankResultItem> RankItems { get; set; }
    }
}
