using TwitterUala.Application.Contracts.Applicaction;
using TwitterUala.Application.Contracts.Infrastructure;
using TwitterUala.Domain.Entities;

namespace TwitterUala.Application.UseCases
{
    public class TweetsFromFollowingByUserService(IFollowingRepository followingRepository/*, IUnitOfWork unitOfWork*/) : ITweetsFromFollowingByUserService
    {
        private readonly IFollowingRepository _followingRepository = followingRepository;
        //private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public List<Tweet> TweetsFromFollowingByUser(long userId)
        {
            // buscar en cache si estan los tweets del usuario

            // si hay tweets, los devuelvo

            var tweets = _followingRepository.TweetsFromFollowingByUserId(userId).ToList();
            //var tweets = _unitOfWork.TweetsFromFollowingByUserId(userId).ToList();

            return tweets;
        }

/*        public IEnumerable<Tweet> TweetsFromFollowingByUserId(long userId)
        {
            //var tweetsByUser = _dbContext.Following.Include(f => f.TweetsUser).Where(f => f.UserId == userId);
            var tweetsByUser = from following in _unitOfWork.GetRepository<Following>().GetListAsync()
                               join tweet in _unitOfWork.GetRepository<Tweet>().GetListAsync() on following.UsersToFollowId equals tweet.UserId
                               where following.UserId == userId
                               orderby tweet.TweetPosted ascending
                               select tweet;
            return tweetsByUser;
        }*/
    }
}
