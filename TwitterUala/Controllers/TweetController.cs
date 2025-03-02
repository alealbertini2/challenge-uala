using Microsoft.AspNetCore.Mvc;
using TwitterUala.Application.Contracts.Applicaction;
using TwitterUala.Domain.Entities;

namespace TwitterUala.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TweetController(ILogger<TweetController> logger, 
        IPublishTweetService publishTweetService,
        ITweetsFromFollowingByUserService tweetsFromFollowingByUserService) : ControllerBase
    {
        private readonly ILogger<TweetController> _logger = logger;
        private readonly ITweetsFromFollowingByUserService _tweetsFromFollowingByUserService = tweetsFromFollowingByUserService;
        private readonly IPublishTweetService _publishTweetService = publishTweetService;

        [HttpPost(Name = "PublishTweet")]
        public void PublishTweet(long userId, string tweetMessage)
        {
            _publishTweetService.PublishTweet(userId, tweetMessage);
        }

        [HttpGet(Name = "FollowingTweetsByUserId")]
        public List<Tweet> FollowingTweetsByUserId(long userId)
        {
            return _tweetsFromFollowingByUserService.TweetsFromFollowingByUser(userId);
        }
    }
}
