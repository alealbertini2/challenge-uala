using TwitterUala.Application.Contracts.Applicaction;
using TwitterUala.Application.Contracts.Infrastructure;
using TwitterUala.Domain.Entities;

namespace TwitterUala.Application.UseCases
{
    public class FollowUserService(IUnitOfWork unitOfWork) : IFollowUserService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task FollowUser(long userId, long userToFollowId)
        {
            Following following = new Following();
            following.UserId = userId;
            following.UsersToFollowId = userToFollowId;

            _unitOfWork.GetRepository<Following>().Add(following);
            _unitOfWork.SaveChangesAsync();
        }
    }
}
