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
            try
            {
                _publishTweetService.PublishTweetAsync(userId, tweetMessage).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al publicar tweet: {ex.Message}", ex);
            }
        }

        [HttpGet(Name = "FollowingTweetsByUserId")]
        public List<Tweet> FollowingTweetsByUserId(long userId)
        {
            try
            {
                return _tweetsFromFollowingByUserService.TweetsFromFollowingByUserAsync(userId).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener tweets del usuario {userId}: {ex.Message}", ex);
            }
        }
    }
}
