using FA.JustBlog.Model;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FA.JustBlog.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Posts> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Posts>().HasData
                (
                    new Posts 
                    { 
                        Id = 1, 
                        Title = "Hello, this my my first post!", 
                        CreatedDate = new DateTime(2022,11,11), 
                        Description= "You may not be aware, but NewBreed relies on donations from gospel partners, like you, so that we can give church planting resources away wherever they’re needed most.\r\nThis year, we’re not asking you to donate. We’re asking you to partner with us in the belief that NewBreed is fulfilling a vital role in training gospel missionaries to penetrate their cultures using 1st century universal principles that empower them to plant anywhere, at anytime, with anyone."
                    },
                    new Posts
                    {
                        Id = 2,
                        Title = "This my my second post!",
                        CreatedDate = new DateTime(2023, 10, 10),
                        Description = "You may not be aware, but NewBreed relies on donations from gospel partners, like you, so that we can give church planting resources away wherever they’re needed most.\r\nThis year, we’re not asking you to donate. We’re asking you to partner with us in the belief that NewBreed is fulfilling a vital role in training gospel missionaries to penetrate their cultures using 1st century universal principles that empower them to plant anywhere, at anytime, with anyone."
                    },
                    new Posts
                    {
                        Id = 3,
                        Title = "This my my third post!",
                        CreatedDate = DateTime.Now,
                        Description = "You may not be aware, but NewBreed relies on donations from gospel partners, like you, so that we can give church planting resources away wherever they’re needed most.\r\nThis year, we’re not asking you to donate. We’re asking you to partner with us in the belief that NewBreed is fulfilling a vital role in training gospel missionaries to penetrate their cultures using 1st century universal principles that empower them to plant anywhere, at anytime, with anyone."
                    }
                );
        }
    }
}
