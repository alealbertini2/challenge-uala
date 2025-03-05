﻿using Microsoft.EntityFrameworkCore;
using TwitterUala.Domain.Entities;

namespace TwitterUala.Infrastructure
{
    public class TwitterDbContext : DbContext
    {
        public TwitterDbContext(DbContextOptions<TwitterDbContext> options)
        : base(options)
        {
        }

        public virtual DbSet<Following> Following { get; set; }
        public virtual DbSet<Tweet> Tweet { get; set; }
        public virtual DbSet<Tweet> User { get; set; }


        public string ToUnderscoreLowerCase(string str)
        {
            return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
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
                entity.HasKey(e => e.IdFollowing);

                entity.Property(e => e.IdFollowing)
                    .ValueGeneratedOnAdd();
                entity.Ignore(f => f.TweetsUser)
                .HasMany(f => f.TweetsUser)
                .WithOne(t => t.Following)
                .HasForeignKey(t => t.UserId);
            });

            modelBuilder.Entity<Tweet>(entity =>
            {
                entity.HasKey(e => e.IdTweet);

                entity.Property(e => e.IdTweet)
                    .ValueGeneratedOnAdd();
                entity.Ignore(t => t.Following);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.Property(e => e.IdUser)
                    .ValueGeneratedOnAdd();
            });
        }
    }
}