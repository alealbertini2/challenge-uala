using Microsoft.EntityFrameworkCore;
using TwitterUala.Application.Contracts.Infrastructure;
using TwitterUala.Domain.Entities;

namespace TwitterUala.Infrastructure.Repositories
{
    public class FollowingRepository(TwitterDbContext dbContext) : IFollowingRepository
    {
        private readonly TwitterDbContext _dbContext = dbContext;

        public void FollowUser(Following follow)
        {
            _dbContext.Following.Add(follow);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Following> TweetsFromFollowingByUserId(long userId)
        {
            var tweetsByUser = _dbContext.Following.Include(f => f.TweetsUser).Where(f => f.UserId == userId);
            return tweetsByUser;
        }
    }
}
