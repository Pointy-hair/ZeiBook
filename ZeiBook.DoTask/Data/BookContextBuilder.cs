using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeiBook.Data;

namespace ZeiBook.DoTask.Data
{
    public class BookContextBuilder
    {
        static DbContextOptionsBuilder<BookDbContext> builder = new DbContextOptionsBuilder<BookDbContext>();

        static BookContextBuilder()
        {
            builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Initial Catalog=Books;Integrated Security=True;");
        }

        public static BookDbContext GetAppContext()
        {
            return new BookDbContext(builder.Options);
        }
    }
}
