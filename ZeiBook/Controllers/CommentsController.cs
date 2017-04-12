using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ZeiBook.Models;
using Microsoft.AspNetCore.Authorization;
using ZeiBook.Data;
using ZeiBook.Models.Results;
using Microsoft.EntityFrameworkCore;
using ZeiBook.Attributes;
using ZeiBook.Actions.Comment;

namespace ZeiBook.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext _context;

        public CommentsController(UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpPost]
        public async Task<JsonResult> Add(int bookId, string content)
        {
            var user = await _userManager.GetUserAsync(User);
            var result = new UpdateResult();
            if (!bookExist(bookId))
            {
                result.Success = false;
                result.Message = "book不存在";
                return Json(result);
            }
            var comment = new Comment {BookId=bookId,Content=content,PostTime=DateTime.Now,UserId=user.Id };
            _context.Comments.Add(comment);
            result.Success = await _context.SaveChangesAsync()>0;
            result.Message = result.Success ? "添加成功" : "添加失败";
            return Json(result);
        }

        [HttpPost("Comments/Remove/{commentId:int}")]
        public async Task<JsonResult> Remove(int commentId)
        {
            (var vaildAction, var comment) = await GetCommentAndVaild(commentId);
            var result = new UpdateResult();
            if (!vaildAction)
            {
                result.Success = false;
                result.Message = "非法操作";
                return Json(result);
            }
             _context.Remove(comment);
            result.Success = await _context.SaveChangesAsync()>0;
            result.Message = result.Success ? "删除成功" : "删除失败";
            return Json(result);
        }

        [AllowAnonymous]
        [HttpGet("Comments/{bookId:int}/p{pageNum:int}")]
        public async Task<JsonResult> Index([FromServices]CommentAction commentAction,int bookId, int pageNum, int pageSize=20)
        {
            if (!bookExist(bookId))
            {
                return Json(null);
            }
            var list = await commentAction.GetCommentList(bookId,pageNum,pageSize);
            return Json(list);
        }

        #region helpers
        private bool bookExist(int bookId)
        {
            return _context.Books.Any(t => t.Id == bookId);
        }

        /// <summary>
        /// 评论操作验证，评论获取
        /// </summary>
        /// <param name="commentId">评论Id</param>
        /// <returns>是否当前用户,评论</returns>
        private async Task<(bool vaildAction, Comment comment)> GetCommentAndVaild(int commentId)
        {
            var user = await _userManager.GetUserAsync(User);
            var comment = _context.Comments.SingleOrDefault(t => t.Id == commentId);
            return (user.Id == comment.UserId&&comment!=null, comment);
        }
        #endregion
    }
}