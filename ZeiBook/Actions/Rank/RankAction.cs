using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeiBook.Data;
using ZeiBook.Models;
using ZeiBook.Models.Utils;

namespace ZeiBook.Actions.Rank
{
    public class RankAction
    {
        private ApplicationDbContext _context;

        public RankAction(ApplicationDbContext context)
        {
            _context = context;
        }

        public void GainBookDownTimes(int bookId)
        {
            var today = DateTime.Now.Date;
            var infoItem = _context.BookDayInfos.SingleOrDefault(t => t.BookId == bookId && t.Day == today);
            if (infoItem==null)
            {
                infoItem = new BookDayInfo { BookId = bookId, Day = today };
                _context.Add(infoItem);
            }
            infoItem.Count++;
            _context.SaveChanges();
        }

        /// <summary>
        /// 更新 特定rank 数据
        /// </summary>
        /// <param name="rankName"></param>
        /// <returns>是否有错</returns>
        public bool BuildRankResult(int rankId)
        {
            var item = _context.BookRanks.SingleOrDefault(t => t.Id == rankId);
            if (item == null)
            {
                return false;
            }
            var today = DateTime.Now.Date;
            var startDate = today.Add(-item.Interval);
            //获取 特定rank 排名集合
            var list = (from bdi in _context.BookDayInfos
                       where bdi.Day >= startDate && bdi.Day < today
                       group bdi by bdi.BookId into g
                       select new { Id = g.Key, Count = g.Sum(t => t.Count) })
                       .OrderByDescending(t=>t.Count).Take(10);
            //清空 特定rank 结果集合
            _context.BookRankResultItems.RemoveRange(
                _context.BookRankResultItems.Where(t => t.BookRankId == item.Id));
            _context.SaveChanges();

            var resultList = new List<BookRankResultItem>();
            //填充结果集合
            foreach (var rankItem in list)
            {
                resultList.Add(new BookRankResultItem
                {
                    BookId = rankItem.Id,
                    BookRankId = item.Id,
                    Position = resultList.Count + 1
                });
            }
            //是否补充
            if (resultList.Count < 10)
            {
                var books = GetTop(10);
                FillList(resultList, 10, item.Id);
            }
            //更新数据库
            _context.BookRankResultItems.AddRange(resultList);
            _context.SaveChanges();
            return true;
        }

        /// <summary>
        /// 填充 排行榜集合
        /// </summary>
        /// <param name="resultList">排行榜集合</param>
        /// <param name="count">目标长度</param>
        /// <param name="rankId">排行榜Id</param>
        private void FillList(List<BookRankResultItem> resultList, int count, int rankId)
        {
            var books = GetTop(count);
            foreach (var book in books)
            {
                if (resultList.Count >= 10)
                {
                    return;
                }
                if (resultList.Any(t => t.BookId == book.Id && t.BookRankId == rankId))
                {
                    continue;
                }
                resultList.Add(new BookRankResultItem
                {
                    BookId = book.Id,
                    BookRankId = rankId,
                    Position = resultList.Count + 1
                });
            }
        }
   
        private List<Models.Book> GetTop(int takeNum)
        {
            return _context.Books.OrderByDescending(b => b.UploadTime).OrderByDescending(b => b.DownloadTime).Take(takeNum).ToList();
        }
    }
}
