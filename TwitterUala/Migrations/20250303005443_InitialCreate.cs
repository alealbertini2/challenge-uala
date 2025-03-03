using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TwitterUala.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "following",
                columns: table => new
                {
                    id_following = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    users_to_follow_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_following", x => x.id_following);
                });

            migrationBuilder.CreateTable(
                name: "tweet",
                columns: table => new
                {
                    id_tweet = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    tweet_message = table.Column<string>(type: "text", nullable: false),
                    tweet_posted = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FollowingIdFollowing = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tweet", x => x.id_tweet);
                    table.ForeignKey(
                        name: "FK_tweet_following_FollowingIdFollowing",
                        column: x => x.FollowingIdFollowing,
                        principalTable: "following",
                        principalColumn: "id_following");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tweet_FollowingIdFollowing",
                table: "tweet",
                column: "FollowingIdFollowing");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tweet");

            migrationBuilder.DropTable(
                name: "following");
        }
    }
}
