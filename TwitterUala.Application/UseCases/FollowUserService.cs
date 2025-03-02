using TwitterUala.Application.Contracts.Applicaction;
using TwitterUala.Application.Contracts.Infrastructure;
using TwitterUala.Domain.Entities;

namespace TwitterUala.Application.UseCases
{
    public class FollowUserService(IFollowingRepository followingRepository) : IFollowUserService
    {
        private readonly IFollowingRepository _followingRepository = followingRepository;

        public void FollowUser(long userId, long userToFollowId)
        {
            Following following = new Following();
            following.UserId = userId;
            following.UsersToFollowId = userToFollowId;
            _followingRepository.FollowUser(following);
        }
    }
}
