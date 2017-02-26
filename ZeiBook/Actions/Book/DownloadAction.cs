using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ZeiBook.Actions.Rank;
using ZeiBook.Data;

namespace ZeiBook.Actions.Book
{
    public class DownloadAction
    {
        private ApplicationDbContext _context;
        private RankAction rankAction;

        public DownloadAction(ApplicationDbContext context, RankAction rankAction)
        {
            _context = context;
            this.rankAction = rankAction;
        }

        public void GainDownloadCount(int id)
        {
            _context.Database.BeginTransaction(IsolationLevel.RepeatableRead);
            var item2 = _context.Books.SingleOrDefault(t => t.Id == id);
            item2.DownloadTime++;
            _context.Update(item2);
            rankAction.GainBookDownTimes(id);
            _context.SaveChanges();
            _context.Database.CommitTransaction();
        }
    }
}
