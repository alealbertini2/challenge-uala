using Microsoft.Extensions.DependencyInjection;
using Moq;
using TwitterUala.Application.Contracts.Applicaction;
using TwitterUala.Application.Contracts.Infrastructure;
using TwitterUala.Application.UseCases;
using TwitterUala.Domain.Entities;
using TwitterUala.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TwitterUalaTest
{
    [TestClass]
    public class UnitTestServices
    {
        public static ServiceProvider serviceProvider;
        public IPublishTweetService _myService;
        [AssemblyInitialize]
        public static void SetUp(TestContext context)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddScoped<IPublishTweetService, PublishTweetService>();
            //build services
            serviceProvider = services.BuildServiceProvider();
        }

        [TestInitialize]
        public void InitializeTest()
        {
            //resolve services
            _myService = serviceProvider.GetService<IPublishTweetService>();
        }

        [TestMethod]
        public void FollowUser_ShouldCallFollowUserMethodInRepository()
        {
/*            // Arrange
            var mockFollowingRepository = new Mock<IFollowingRepository>();
            var followUserService = new FollowUserService(mockFollowingRepository.Object);
            var tweetsFromFollowingByUserService = new TweetsFromFollowingByUserService(mockFollowingRepository.Object);

            var userId = 1L;
            var userToFollowId = 2L;

            // Act
            followUserService.FollowUser(userId, userToFollowId);

            // Assert
            mockFollowingRepository.Verify(
                repo => repo.FollowUser(It.Is<Following>(f => f.UserId == userId && f.UsersToFollowId == userToFollowId)),
                Times.Once
            );

            var tweetsByUser1 = tweetsFromFollowingByUserService.TweetsFromFollowingByUser(userId);
            var tweetsByUser2 = tweetsFromFollowingByUserService.TweetsFromFollowingByUser(userToFollowId);
            Assert.IsTrue(tweetsByUser1.Count == 0);
            Assert.IsTrue(tweetsByUser2.Count == 0);*/
        }

        [TestMethod]
        public void FollowUser_ShouldCallFollowUserMethodInRepository2()
        {

/*            // Arrange
            var mockFollowingRepository = new Mock<IFollowingRepository>();
            var mockTweetRepository = new Mock<ITweetRepository>();
            var followUserService = new FollowUserService(mockFollowingRepository.Object);
            var tweetsFromFollowingByUserService = new TweetsFromFollowingByUserService(mockFollowingRepository.Object);
            var publishTweetService = _myService;

            var dbContext = new TwitterDbContext();
            Mock.Arrange(() => dbContext.Tweet).ReturnsCollection(FakeTweets());

            var user1 = 1L;
            var user2 = 2L;

            followUserService.FollowUser(user1, user2);

            string tweetMessage = "User 2 tuit 1";
            publishTweetService.PublishTweet(user2, tweetMessage);

            // Crear una lista ficticia de tweets que devolveremos al llamar al método mockeado
            var fakeTweetsUser2 = new List<Tweet>
            {
                new Tweet { UserId = 2, TweetMessage = "User 2 tuit 1", TweetPosted = DateTime.UtcNow }
            };

            // Configuramos el mock para que el método 'TweetsFromFollowingByUserId' devuelva los tweets ficticios
            mockFollowingRepository.Setup(repo => repo.TweetsFromFollowingByUserId(user2)).Returns(fakeTweetsUser2);

            var tweetsByUser1 = tweetsFromFollowingByUserService.TweetsFromFollowingByUser(user1);
            var tweetsByUser2 = tweetsFromFollowingByUserService.TweetsFromFollowingByUser(user2);
            Assert.IsTrue(tweetsByUser1.Count == 0);
            Assert.IsTrue(tweetsByUser2.Count == 1);

            var user3 = 3L;
            followUserService.FollowUser(user1, user3);
            publishTweetService.PublishTweet(user2, "User 2 tuit 2");
            publishTweetService.PublishTweet(user3, "User 3 tuit 1");

            Tweet tweetUser2 = new Tweet { UserId = 2, TweetMessage = "User 2 tuit 2", TweetPosted = DateTime.UtcNow };
            fakeTweetsUser2.Add(tweetUser2);

            var fakeTweetsUser3 = new List<Tweet>
            {
                new Tweet { UserId = 3, TweetMessage = "User 3 tuit 1", TweetPosted = DateTime.UtcNow }
            };

            mockFollowingRepository.Setup(repo => repo.TweetsFromFollowingByUserId(user2)).Returns(fakeTweetsUser2);
            mockFollowingRepository.Setup(repo => repo.TweetsFromFollowingByUserId(user3)).Returns(fakeTweetsUser3);

            tweetsByUser1 = tweetsFromFollowingByUserService.TweetsFromFollowingByUser(user1);
            tweetsByUser2 = tweetsFromFollowingByUserService.TweetsFromFollowingByUser(user2);
            var tweetsByUser3 = tweetsFromFollowingByUserService.TweetsFromFollowingByUser(user3);

            Assert.IsTrue(tweetsByUser1.Count == 0);
            Assert.IsTrue(tweetsByUser2.Count == 2);
            Assert.IsTrue(tweetsByUser3.Count == 1);*/
        }
    }
}