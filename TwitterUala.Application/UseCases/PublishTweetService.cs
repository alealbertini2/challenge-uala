using TwitterUala.Application.Contracts.Applicaction;
using TwitterUala.Application.Contracts.Infrastructure;
using TwitterUala.Domain.Entities;

namespace TwitterUala.Application.UseCases
{
    public class PublishTweetService(ITweetRepository tweetRepository) : IPublishTweetService
    {
        private readonly ITweetRepository _tweetRepository = tweetRepository;

        public void PublishTweet(long userId, string tweetMessage)
        {
            Tweet tweet = new Tweet();
            tweet.UserId = userId;
            tweet.TweetMessage = tweetMessage;
            tweet.TweetPosted = DateTime.UtcNow;
            _tweetRepository.PublishTweet(tweet);
        }
    }
}
