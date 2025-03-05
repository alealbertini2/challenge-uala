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
            var validUser = _unitOfWork.GetRepository<User>().FirstOrDefaultAsync(u => u.IdUser == userId);
            if (validUser == null) 
            {
                throw new Exception("El usuario actual no es válido");
            }

            var validUserToFollow = _unitOfWork.GetRepository<User>().FirstOrDefaultAsync(u => u.IdUser == userToFollowId);
            if (validUserToFollow == null)
            {
                throw new Exception("El usuario a seguir no es válido");
            }

            Following following = new Following();
            following.UserId = userId;
            following.UsersToFollowId = userToFollowId;

            _unitOfWork.GetRepository<Following>().Add(following);
            _unitOfWork.SaveChangesAsync();
        }
    }
}
