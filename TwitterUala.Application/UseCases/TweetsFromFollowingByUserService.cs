using TwitterUala.Application.Contracts.Applicaction;
using TwitterUala.Application.Contracts.Infrastructure;
using TwitterUala.Domain.Entities;

namespace TwitterUala.Application.UseCases
{
    public class TweetsFromFollowingByUserService(IFollowingRepository followingRepository) : ITweetsFromFollowingByUserService
    {
        private readonly IFollowingRepository _followingRepository = followingRepository;

        public List<Tweet> TweetsFromFollowingByUser(long userId)
        {
            // buscar en cache si estan los tweets del usuario

            // si hay tweets, los devuelvo

            var tweets = _followingRepository.TweetsFromFollowingByUserId(userId);
            List<Tweet> tweetsByFollowingUsers = new List<Tweet>();
            tweets.ToList().ForEach(t => tweetsByFollowingUsers.AddRange(t.TweetsUser));
            return tweetsByFollowingUsers;
        }
    }
}
