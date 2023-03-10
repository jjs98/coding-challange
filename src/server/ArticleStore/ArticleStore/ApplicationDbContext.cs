using Microsoft.EntityFrameworkCore;
using System;

namespace ArticleStore
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<AggregatedArticle> Articles { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
