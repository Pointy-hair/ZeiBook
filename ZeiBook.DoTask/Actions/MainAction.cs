using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZeiBook.Data;
using ZeiBook.DoTask.Data;
using ZeiBook.DoTask.Jobs;

namespace ZeiBook.DoTask.Actions
{
    class MainAction
    {
        public async Task DoScheduleAsync()
        {
            var scheduler = await StdSchedulerFactory.GetDefaultScheduler();

            await scheduler.Start();


            using (ApplicationDbContext db = ContextBuilder.GetAppContext())
            {
                foreach (var rankItem in db.BookRanks)
                {
                    try
                    {
                        if (!rankItem.EnableTask) continue;
                        var map = new JobDataMap();
                        map.Add("RankItem", rankItem);
                        var jobTmp = JobBuilder.Create<RankJob>()
                            .WithIdentity(rankItem.Name + "_job", "group1")
                            .UsingJobData(map)
                            .Build();
                        var triggerTmp = TriggerBuilder.Create()
                            .WithIdentity(rankItem.Name + "_trigger", "group1")
                            .WithCronSchedule(rankItem.CornValue)
                            .Build();
                        await scheduler.ScheduleJob(jobTmp, triggerTmp);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
        }
    }
}
