using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TwitterUala.Application.Contracts.Applicaction;
using TwitterUala.Application.Contracts.Infrastructure;
using TwitterUala.Application.UseCases;
using TwitterUala.Infrastructure.Impl;
using TwitterUala.Infrastructure.Repositories;

namespace TwitterUalaTest
{
    [TestClass]
    public class CreateUserTestServices
    {
        public static ServiceProvider _serviceProvider;

        [TestInitialize]
        public void Setup()
        {
            var services = new ServiceCollection();

            // Using In-Memory database for testing
            services.AddDbContext<DbInMemoryContext>(options =>
                options.UseInMemoryDatabase("TestDb"));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<DbContext, DbInMemoryContext>();

            services.AddLogging();

            services.AddScoped<IFollowUserService, FollowUserService>();
            services.AddScoped<IPublishTweetService, PublishTweetService>();
            services.AddScoped<ITweetsFromFollowingByUserService, TimelineService>();
            services.AddScoped<ICreateUserService, CreateUserService>();
            services.AddScoped<IFollowingRepository, FollowingRepository>();

            services.AddExceptionHandler<ExceptionHandler>();

            _serviceProvider = services.BuildServiceProvider();
        }

        /* public UnitTestServices() 
         {
             using (var context = new DbInMemoryContext())
             {
                 var followings = new List<Following>
                 {
                     new Following
                     {
                         UserId = 1,
                         UsersToFollowId = 2
                     },
                     new Following
                     {
                         UserId = 1,
                         UsersToFollowId = 3
                     }
                 };

                 var tweets = new List<Tweet>
                 {
                     new Tweet
                     {
                         UserId = 2,
                         TweetMessage = "tuit 1 user 2",
                         TweetPosted = DateTime.Now
                     },
                     new Tweet
                     {
                         UserId = 3,
                         TweetMessage = "tuit 1 user 3",
                         TweetPosted = DateTime.Now
                     },
                     new Tweet
                     {
                         UserId = 3,
                         TweetMessage = "tuit 2 user 3",
                         TweetPosted = DateTime.Now
                     },
                     new Tweet
                     {
                         UserId = 2,
                         TweetMessage = "tuit 2 user 2",
                         TweetPosted = DateTime.Now
                     }
                 };

                 context.Following.AddRange(followings);
                 context.Tweet.AddRange(tweets);
                 context.SaveChanges();
             }
         }   */

        /*        [TestMethod]
                public void TestDbInMemory()
                {
                    using (var context = new DbInMemoryContext())
                    {
                        var followingList = context.Following.ToList();
                        var tweetList = context.Tweet.ToList();
                    }
                }*/

        [TestMethod]
        public void CreateUser_ShouldInsertUserOk()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var manager = scopedServices.GetRequiredService<ICreateUserService>();
                var dbContext = scopedServices.GetRequiredService<DbInMemoryContext>();

                string username = "Benedicto";
                manager.CreateUserAsync(username);

                // Assert
                var addedItem = dbContext.User.Find((long)1);
                Assert.IsNotNull(addedItem);
                Assert.AreEqual(username, addedItem.Username);
            }
        }

        [TestMethod]
        public async Task CreateUserAsync_ThrowsInvalidDataException_WhenUsernameIsEmpty()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var manager = scopedServices.GetRequiredService<ICreateUserService>();
                var dbContext = scopedServices.GetRequiredService<DbInMemoryContext>();

                string username = "";
                var exception = await Assert.ThrowsExceptionAsync<InvalidDataException>(() => manager.CreateUserAsync(username));
                Assert.AreEqual("El nombre del usuario no puede ser vacío", exception.Message);
            }
        }

        [TestMethod]
        public async Task CreateUserAsync_ThrowsInvalidDataException_WhenUsernameIsTooLongAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var manager = scopedServices.GetRequiredService<ICreateUserService>();
                var dbContext = scopedServices.GetRequiredService<DbInMemoryContext>();

                string longUsername = new string('a', 51);
                var exception = await Assert.ThrowsExceptionAsync<InvalidDataException>(() => manager.CreateUserAsync(longUsername));
                Assert.AreEqual("El nombre del usuario no puede ser mayor a 50 caracteres", exception.Message);
            }
        }

        [TestMethod]
        public async Task CreateUserAsync_ThrowsInvalidDataException_WhenUsernameAlreadyExists()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var manager = scopedServices.GetRequiredService<ICreateUserService>();
                var dbContext = scopedServices.GetRequiredService<DbInMemoryContext>();

                string username = "Benedicto";
                manager.CreateUserAsync(username);

                var exception = await Assert.ThrowsExceptionAsync<InvalidDataException>(() => manager.CreateUserAsync(username));
                Assert.AreEqual("Ya existe un usuario con el mismo nombre de usuario", exception.Message);
            }
        }


        /*        [TestMethod]
                public void FollowUser_ShouldCallFollowUserMethodInRepository()
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        // Arrange
                        var mockFollowingRepository = new Mock<IFollowingRepository>();
                        var mockUnitOfWork = new Mock<IUnitOfWork>();
                        var mockLog = new Mock<ILogger<FollowUserService>>();
                        var mockLogTimelineService = new Mock<ILogger<TimelineService>>();
                        var followUserService = new FollowUserService(mockUnitOfWork.Object, mockLog.Object);
                        var tweetsFromFollowingByUserService = new TimelineService(mockFollowingRepository.Object, mockUnitOfWork.Object, mockLogTimelineService.Object);

                        var userId = 1L;
                        var userToFollowId = 2L;

                        // Act
                        followUserService.FollowUserAsync(userId, userToFollowId);

                        // Assert
                        *//*            mockFollowingRepository.Verify(
                                        repo => repo.FollowUser(It.Is<Following>(f => f.UserId == userId && f.UsersToFollowId == userToFollowId)),
                                        Times.Once
                                    );*//*

                        var tweetsByUser1 = tweetsFromFollowingByUserService.TimelineByUserIdAsync(userId).GetAwaiter().GetResult();
                        var tweetsByUser2 = tweetsFromFollowingByUserService.TimelineByUserIdAsync(userToFollowId).GetAwaiter().GetResult();
                        Assert.IsTrue(tweetsByUser1.Count == 0);
                        Assert.IsTrue(tweetsByUser2.Count == 0);
                    }
                }*/

        /*[TestMethod]
        public void FollowUser_ShouldCallFollowUserMethodInRepository2()
        {

            // Arrange
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
            Assert.IsTrue(tweetsByUser3.Count == 1);
        }*/
    }
}