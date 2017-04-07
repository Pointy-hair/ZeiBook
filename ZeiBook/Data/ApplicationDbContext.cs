using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZeiBook.Models;

namespace ZeiBook.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
            builder.Entity<BookRankResultItem>().HasKey(t => new { t.BookRankId, t.BookId });
            builder.Entity<Book>().HasIndex(t=>new { t.AuthorId,t.Name}).IsUnique();
            builder.Entity<Author>().HasIndex(c => c.Name).IsUnique();
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookRank> BookRanks { get; set; }
        public DbSet<BookDayInfo> BookDayInfos { get; set; }
        public DbSet<BookRankResultItem> BookRankResultItems { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
