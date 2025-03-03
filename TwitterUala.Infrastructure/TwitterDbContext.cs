using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TwitterUala.Domain.Entities;

namespace TwitterUala.Infrastructure
{
    public class TwitterDbContext : DbContext
    {
        /*        private readonly IConfiguration _configuration;
                public TwitterDbContext(IConfiguration configuration)
                {
                    _configuration = configuration;
                }*/

        public TwitterDbContext(DbContextOptions<TwitterDbContext> options/*, IConfiguration configuration*/)
        : base(options)
        {
            // _configuration = configuration;
        }

        public virtual DbSet<Following> Following { get; set; }

        public virtual DbSet<Tweet> Tweet { get; set; }

        /*        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                {
                    string connString = $"Host=localhost;Port=5432;User ID=postgres;Password=uala123;Database=twitter_uala;";
                    optionsBuilder.UseNpgsql(connString);
                }*/

        public string ToUnderscoreLowerCase(string str)
        {
            return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var tables = new List<Type> { typeof(Tweet), typeof(Following) };

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
                entity.HasKey(e => e.IdFollowing)/*.HasName("ID_pkey")*/;

                //entity.ToTable("following");
                entity.Property(e => e.IdFollowing)
                    .ValueGeneratedOnAdd();
                entity.Ignore(f => f.TweetsUser)
                .HasMany(f => f.TweetsUser)
                .WithOne(t => t.Following)
                .HasForeignKey(t => t.UserId);
                //.HasColumnName("id");
                //entity.Property(e => e.UserId).HasColumnName("user_id");
                //entity.Property(e => e.UsersToFollowId).HasColumnName("users_to_follow_id");
            });

            modelBuilder.Entity<Tweet>(entity =>
            {
                entity.HasKey(e => e.IdTweet)/*.HasName("TWEET_pkey")*/;

                //entity.ToTable("tweet");

                entity.Property(e => e.IdTweet)
                    .ValueGeneratedOnAdd();
                //.HasColumnName("id");
                //entity.Property(e => e.UserId).HasColumnName("user_id");
                //entity.Property(e => e.TweetMessage).HasColumnName("tweet_message");
               // entity.Property(e => e.TweetPosted)
                    //.HasColumnType("timestamp without time zone");
                entity.Ignore(t => t.Following);
                //.HasColumnName("tweet_posted");
            });
        }
    }
}