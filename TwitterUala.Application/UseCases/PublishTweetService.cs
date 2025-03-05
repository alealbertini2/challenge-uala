using TwitterUala.Application.Contracts.Applicaction;
using TwitterUala.Application.Contracts.Infrastructure;
using TwitterUala.Domain.Entities;

namespace TwitterUala.Application.UseCases
{
    public class PublishTweetService(IUnitOfWork unitOfWork) : IPublishTweetService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork; 

        public void PublishTweet(long userId, string tweetMessage)
        {
            Tweet tweet = new Tweet();
            tweet.UserId = userId;
            tweet.TweetMessage = tweetMessage;
            tweet.TweetPosted = DateTime.UtcNow;

            _unitOfWork.GetRepository<Tweet>().Add(tweet);
            _unitOfWork.SaveChangesAsync();
        }
    }
}
