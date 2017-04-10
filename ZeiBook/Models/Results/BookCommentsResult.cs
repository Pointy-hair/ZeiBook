using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeiBook.Models.Utils;

namespace ZeiBook.Models.Results
{
    public class BookCommentsResult
    {
        public bool Success { get; set; }
        public List<CommentItem> Comments { get; set; }
        public PageOption PageOption { get; set; }
    }

    public class CommentItem
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }
        public DateTime PostTime { get; set; }
    }
}
