using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeiBook.Data;

namespace ZeiBook.DoTask.Data
{
    public class ContextBuilder
    {
        static DbContextOptionsBuilder<ApplicationDbContext> builder = new DbContextOptionsBuilder<ApplicationDbContext>();

        static ContextBuilder()
        {
            builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aspnet-ZeiBook-99061064-7655-4520-a500-f61b335e6659;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        public static ApplicationDbContext GetAppContext()
        {
            return new ApplicationDbContext(builder.Options);
        }
    }
}
