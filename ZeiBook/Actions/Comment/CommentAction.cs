using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeiBook.Data;
using ZeiBook.Models.Results;
using ZeiBook.Models.Utils;

namespace ZeiBook.Actions.Comment
{
    public class CommentAction
    {
        private ApplicationDbContext _context;

        public CommentAction(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BookCommentsResult> GetCommentList(int bookId, int pageNum, int pageSize)
        {
            var pageOption = new PageOption { PageNum = pageNum, PageSize = pageSize };
            var collection = _context.Comments.Where(t => t.BookId == bookId);
            pageOption.PageCount = (int)Math.Ceiling(collection.Count() / (double)pageSize);

            if (!pageOption.Valid())
            {
                return new BookCommentsResult { Success = false };
            }

            List<CommentItem> comments = new List<CommentItem>();
            var list = await collection.Include(t => t.User).OrderBy(t => t.PostTime).Skip((pageNum - 1) * pageSize).Take(pageSize).ToListAsync();
            list.ForEach(t=> {
                comments.Add(new CommentItem
                {
                    Id = t.Id,
                    Content = t.Content,
                    PostTime = t.PostTime,
                    UserName = t.User.UserName,
                    UserId = t.UserId
                });
            });
            return new BookCommentsResult
            {
                Success = true,
                PageOption = pageOption,
                Comments = comments
            };
        }
    }
}
