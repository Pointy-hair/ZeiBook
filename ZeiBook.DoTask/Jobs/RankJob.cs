using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeiBook.Actions.Rank;
using ZeiBook.DoTask.Data;
using ZeiBook.Models;

namespace ZeiBook.DoTask.Jobs
{
    public class RankJob:IJob
    {

        public async Task Execute(IJobExecutionContext context)
        {
            var map = context.JobDetail.JobDataMap;
            var rankItem = map.Get("rankItem") as BookRank;
            Console.WriteLine("{0} is start", rankItem.Name);
            var db = ContextBuilder.GetAppContext();
            var action = new RankAction(db);
            action.BuildRankResult(rankItem.Id);
            await Console.Out.WriteLineAsync(string.Format("{0} is updated", rankItem.Name));
        }
    }
}
