using FA.JustBlog.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FA.JustBlog.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Posts> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagPost> TagsPost { get; set; }
        public DbSet<InterestPost> InterestPosts { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Comment> Comments { get; set; }

        private void SeedUsers(ModelBuilder modelBuilder)
        {
            ApplicationUser user = new ApplicationUser()
            {
                Id = "b74ddd14-6340-4840-95c2-db12554843e9",
                UserName = "admin@gmail.com",
                NormalizedUserName = "admin@gmail.com",
                NormalizedEmail = "admin@gmail.com",
                Email = "admin@gmail.com",
                LockoutEnabled = false,
                PhoneNumber = "0000000000",
                EmailConfirmed = true,
                Name = "Owner Blog"
            };
            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHasher.HashPassword(user, "Admin123");
            modelBuilder.Entity<ApplicationUser>().HasData(user);
        }

        private void SeedUserRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>()
                {
                    RoleId = "4582a3e7-b236-4a79-9f6c-929aa61b9ec8",
                    UserId = "b74ddd14-6340-4840-95c2-db12554843e9"
                }
            );
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            this.SeedUsers(modelBuilder);
            this.SeedUserRoles(modelBuilder);

            // CHANGE NAME IDENTITY ASP.NET
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }
            // RELATION
            modelBuilder.Entity<Posts>().HasOne(x => x.Categories)
                .WithMany(x => x.Posts)
                .HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<TagPost>().HasKey(x => new { x.PostId, x.TagId });
            modelBuilder.Entity<TagPost>().HasOne<Posts>(x => x.Post).WithMany(x => x.TagPosts).HasForeignKey(x => x.PostId);
            modelBuilder.Entity<TagPost>().HasOne<Tag>(x => x.Tags).WithMany(x => x.TagPosts).HasForeignKey(x => x.TagId);

            modelBuilder.Entity<InterestPost>().HasOne(x => x.Post).WithMany(x => x.InterestPosts).HasForeignKey(x => x.PostId);

            modelBuilder.Entity<Comment>().HasOne(x => x.Users).WithMany(x => x.Comments).HasForeignKey(x => x.userId);
            //SEED DATA
            modelBuilder.Entity<Tag>().HasData(
                new Tag
                {
                    TagId = 1,
                    Name = "ado.net"
                },
                new Tag
                {
                    TagId = 2,
                    Name = "javascript"
                },
                new Tag
                {
                    TagId = 3,
                    Name = "mvc"
                },
                new Tag
                {
                    TagId = 4,
                    Name = "Csharp"
                },
                new Tag
                {
                    TagId = 5,
                    Name = "entity framework"
                });
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = 1,
                    Name = "Entity Framework"
                },
                new Category
                {
                    CategoryId = 2,
                    Name = "MVC"
                });
            modelBuilder.Entity<Posts>().HasData(
                new Posts
                {
                    Id = 1,
                    Title = "Hello, this my my first post!",
                    CreatedDate = new DateTime(2022, 11, 11),
                    Description = "You may not be aware, but NewBreed relies on donations from gospel partners, like you, so that we can give church planting resources away wherever they’re needed most.\r\nThis year, we’re not asking you to donate. We’re asking you to partner with us in the belief that NewBreed is fulfilling a vital role in training gospel missionaries to penetrate their cultures using 1st century universal principles that empower them to plant anywhere, at anytime, with anyone.",
                    Content = "This is some additional paragraph placeholder content. It has been written to fill the available space and show how a longer snippet of text affects the surrounding content. We'll repeat it often to keep the demonstration flowing, so be on the lookout for this exact same string of text.\r\n\r\nLonger quote goes here, maybe with some emphasized text in the middle of it.\r\n\r\nThis is some additional paragraph placeholder content. It has been written to fill the available space and show how a longer snippet of text affects the surrounding content. We'll repeat it often to keep the demonstration flowing, so be on the lookout for this exact same string of text.",
                    ViewCount = 3,
                    isPublised = true,
                    CategoryId = 1,
                },
                new Posts
                {
                    Id = 2,
                    Title = "This my my second post!",
                    CreatedDate = new DateTime(2023, 10, 10),
                    Description = "You may not be aware, but NewBreed relies on donations from gospel partners, like you, so that we can give church planting resources away wherever they’re needed most.\r\nThis year, we’re not asking you to donate. We’re asking you to partner with us in the belief that NewBreed is fulfilling a vital role in training gospel missionaries to penetrate their cultures using 1st century universal principles that empower them to plant anywhere, at anytime, with anyone.",
                    Content = "This is some additional paragraph placeholder content. It has been written to fill the available space and show how a longer snippet of text affects the surrounding content. We'll repeat it often to keep the demonstration flowing, so be on the lookout for this exact same string of text.\r\n\r\nLonger quote goes here, maybe with some emphasized text in the middle of it.\r\n\r\nThis is some additional paragraph placeholder content. It has been written to fill the available space and show how a longer snippet of text affects the surrounding content. We'll repeat it often to keep the demonstration flowing, so be on the lookout for this exact same string of text.",
                    ViewCount = 5,
                    isPublised = true,
                    CategoryId = 2,
                },
                new Posts
                {
                    Id = 3,
                    Title = "This my my third post!",
                    CreatedDate = DateTime.Now,
                    Description = "You may not be aware, but NewBreed relies on donations from gospel partners, like you, so that we can give church planting resources away wherever they’re needed most.\r\nThis year, we’re not asking you to donate. We’re asking you to partner with us in the belief that NewBreed is fulfilling a vital role in training gospel missionaries to penetrate their cultures using 1st century universal principles that empower them to plant anywhere, at anytime, with anyone.",
                    Content = "This is some additional paragraph placeholder content. It has been written to fill the available space and show how a longer snippet of text affects the surrounding content. We'll repeat it often to keep the demonstration flowing, so be on the lookout for this exact same string of text.\r\n\r\nLonger quote goes here, maybe with some emphasized text in the middle of it.\r\n\r\nThis is some additional paragraph placeholder content. It has been written to fill the available space and show how a longer snippet of text affects the surrounding content. We'll repeat it often to keep the demonstration flowing, so be on the lookout for this exact same string of text.",
                    ViewCount = 1,
                    isPublised = true,
                    CategoryId = 1,
                });
            modelBuilder.Entity<InterestPost>().HasData(
                new InterestPost
                {
                    InterestId = 1,
                    Rate = 10,
                    PostId = 1,
                },
                new InterestPost
                {
                    InterestId = 2,
                    Rate = 8,
                    PostId = 1,
                },
                new InterestPost
                {
                    InterestId = 3,
                    Rate = 7,
                    PostId = 1,
                },
                new InterestPost
                {
                    InterestId = 4,
                    Rate = 7,
                    PostId = 2,
                },
                new InterestPost
                {
                    InterestId = 5,
                    Rate = 5,
                    PostId = 2,
                },
                new InterestPost
                {
                    InterestId = 6,
                    Rate = 10,
                    PostId = 3,
                },
                new InterestPost
                {
                    InterestId = 7,
                    Rate = 4,
                    PostId = 3,
                });
            modelBuilder.Entity<TagPost>().HasData(
                new TagPost
                {
                    PostId = 1,
                    TagId = 1,
                },
                new TagPost
                {
                    PostId = 1,
                    TagId = 2,
                },
                new TagPost
                {
                    PostId = 1,
                    TagId = 3,
                },
                new TagPost
                {
                    PostId = 1,
                    TagId = 4,
                },
                new TagPost
                {
                    PostId = 1,
                    TagId = 5,
                },
                new TagPost
                {
                    PostId = 2,
                    TagId = 3,
                },
                new TagPost
                {
                    PostId = 2,
                    TagId = 5,
                },
                new TagPost
                {
                    PostId = 3,
                    TagId = 2,
                },
                new TagPost
                {
                    PostId = 3,
                    TagId = 5,
                },
                new TagPost
                {
                    PostId = 3,
                    TagId = 1,
                }
                );
        }
    }
}
