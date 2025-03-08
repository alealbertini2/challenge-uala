using TwitterUala.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using TwitterUala.Infrastructure;

namespace TwitterUalaTest
{
    public class DbInMemoryContext : DbContext
    {

        public DbInMemoryContext(DbContextOptions<DbInMemoryContext> options)
        : base(options)
        {
        }

        public DbSet<Following> Following { get; set; }
        public DbSet<Tweet> Tweet { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "twitter_uala");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var tables = new List<Type> { typeof(Tweet), typeof(Following), typeof(User) };

            foreach (var table in tables)
            {
                foreach (var column in table.GetProperties())
                {
                    modelBuilder.Entity(table).Property(column.Name).HasColumnName(ToUnderscoreLowerCase(column.Name));
                }
                modelBuilder.Entity(table).ToTable(ToUnderscoreLowerCase(table.Name));
            }

            modelBuilder.Entity<Tweet>().Ignore(t => t.Following);

            modelBuilder.Entity<Following>(entity =>
            {
                entity.HasKey(f => new { f.UserId, f.UsersToFollowId });

                entity.HasIndex(e => e.UsersToFollowId, "IX_Following_UsersToFollowId");

                entity.HasIndex(e => new { e.UserId, e.UsersToFollowId }, "IX_Following_UserId_UsersToFollowId");

                /*                entity.HasKey(e => e.IdFollowing);

                                entity.Property(e => e.IdFollowing)
                                    .ValueGeneratedOnAdd();*/
                entity.Ignore(f => f.TweetsUser)
                .HasMany(f => f.TweetsUser)
                .WithOne(t => t.Following)
                .HasForeignKey(t => t.UserId);
            });

            modelBuilder.Entity<Tweet>(entity =>
            {
                entity.HasKey(e => e.IdTweet);

                entity.HasIndex(t => t.UserId, "IX_Tweet_UserId");

                entity.Property(e => e.IdTweet)
                    .ValueGeneratedOnAdd();
                entity.Ignore(t => t.Following);
                entity.Property(e => e.TweetMessage)
                    .HasMaxLength(280);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.HasIndex(t => t.IdUser, "IX_User_IdUser");

                entity.Property(e => e.IdUser)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Username)
                    .HasMaxLength(50);
            });
        }

        public static string ToUnderscoreLowerCase(string str)
        {
            return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
        }
    }
}
