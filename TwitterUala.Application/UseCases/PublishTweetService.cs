using TwitterUala.Application.Contracts.Applicaction;
using TwitterUala.Application.Contracts.Infrastructure;
using TwitterUala.Domain.Entities;

namespace TwitterUala.Application.UseCases
{
    public class PublishTweetService(IUnitOfWork unitOfWork) : IPublishTweetService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork; 

        public async Task PublishTweetAsync(long userId, string tweetMessage)
        {
            try
            {
                var validUser = await _unitOfWork.GetRepository<User>().FirstOrDefaultAsync(u => u.IdUser == userId);
                if (validUser == null)
                {
                    throw new Exception("El usuario actual no es válido");
                }

                if (tweetMessage.Length <= 0)
                {
                    throw new Exception("El mensaje no puede ser vacío");
                }

                if (tweetMessage.Length > 280)
                {
                    throw new Exception("El mensaje no puede ser mayor a 280 caracteres");
                }

                Tweet tweet = new Tweet();
                tweet.UserId = userId;
                tweet.TweetMessage = tweetMessage;
                tweet.TweetPosted = DateTime.UtcNow;

                _unitOfWork.GetRepository<Tweet>().Add(tweet);
                _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex) 
            { 
                throw new Exception(ex.Message);
            }
        }
    }
}
