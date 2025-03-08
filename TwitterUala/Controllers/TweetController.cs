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
            _logger.LogInformation("Se publicará el tweet {0} para el usuario: {1}", tweetMessage, userId);
            _publishTweetService.PublishTweetAsync(userId, tweetMessage);
        }

        [HttpGet(Name = "FollowingTweetsByUserId")]
        public async Task<List<Tweet>> FollowingTweetsByUserId(long userId)
        {
            _logger.LogInformation("Obtener tweets para el usuario: {0}", userId);
            return await _tweetsFromFollowingByUserService.TweetsFromFollowingByUserAsync(userId);
        }
    }
}
