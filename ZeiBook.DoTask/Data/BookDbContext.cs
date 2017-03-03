using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZeiBook.Models;
using ZeiBook.DoTask.Models;

namespace ZeiBook.DoTask.Data
{
    public class BookDbContext:DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BookData>().HasKey(t => new { t.AuthorName, t.Title });
        }

        public DbSet<BookData> BookDatas { get; set; }
    }
}
