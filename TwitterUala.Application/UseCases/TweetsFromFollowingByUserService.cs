using TwitterUala.Application.Contracts.Applicaction;
using TwitterUala.Application.Contracts.Infrastructure;
using TwitterUala.Domain.Entities;

namespace TwitterUala.Application.UseCases
{
    public class TweetsFromFollowingByUserService(IFollowingRepository followingRepository, IUnitOfWork unitOfWork) : ITweetsFromFollowingByUserService
    {
        private readonly IFollowingRepository _followingRepository = followingRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public List<Tweet> TweetsFromFollowingByUser(long userId)
        {
            var validUser = _unitOfWork.GetRepository<User>().FirstOrDefaultAsync(u => u.IdUser == userId);
            if (validUser == null)
            {
                throw new Exception("El usuario actual no es válido");
            }

            // buscar en cache si estan los tweets del usuario

            // si hay tweets, los devuelvo

            var tweets = _followingRepository.TweetsFromFollowingByUserId(userId).ToList();
            return tweets;
        }
    }
}
