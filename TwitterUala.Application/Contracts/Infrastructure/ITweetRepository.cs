using TwitterUala.Domain.Entities;

namespace TwitterUala.Application.Contracts.Infrastructure
{
    public interface ITweetRepository
    {
        void PublishTweet(Tweet tweet);
    }
}