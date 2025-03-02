using Microsoft.EntityFrameworkCore;
using TwitterUala.Application.Contracts.Infrastructure;
using TwitterUala.Domain.Entities;

namespace TwitterUala.Infrastructure.Repositories
{
    public class TweetRepository(TwitterDbContext dbContext) : ITweetRepository
    {
        private readonly TwitterDbContext _dbContext = dbContext;

        public void PublishTweet(Tweet tweet)
        {
            _dbContext.Tweet.Add(tweet);
            _dbContext.SaveChanges();
        }
    }
}
