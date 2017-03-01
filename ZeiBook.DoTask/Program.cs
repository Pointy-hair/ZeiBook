using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZeiBook.DoTask.Actions;
using ZeiBook.DoTask.Jobs;
using ZeiBook.DoTask.Loggers;

namespace ZeiBook.DoTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            LogProvider.SetCurrentLogProvider(new ConsoleLogProvider());
            MainAction action = new MainAction();
            action.DoScheduleAsync().GetAwaiter().GetResult();
            Console.ReadKey();
        }
    }
}
