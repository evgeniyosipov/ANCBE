using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ANCBE.Models
{
    public class BlogDataContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }

        public BlogDataContext(DbContextOptions<BlogDataContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public IQueryable<ArchivedPostsSummary> GetArchivedPosts()
        {
            return
                Posts
                    .GroupBy(x => new { x.PostedDate.Year, x.PostedDate.Month })
                    .Select(group => new ArchivedPostsSummary
                    {
                        Count = group.Count(),
                        Year = group.Key.Year,
                        Month = group.Key.Month,
                    });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ForSqlServerUseIdentityColumns();
        }
    }
}
