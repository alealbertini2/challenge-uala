namespace TwitterUala.Application.Contracts.Applicaction
{
    public interface IPublishTweetService
    {
        void PublishTweet(long userId, string tweetMessage);
    }
}