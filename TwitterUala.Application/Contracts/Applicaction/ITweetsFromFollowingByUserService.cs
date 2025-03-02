using TwitterUala.Domain.Entities;

namespace TwitterUala.Application.Contracts.Applicaction
{
    public interface ITweetsFromFollowingByUserService
    {
        List<Tweet> TweetsFromFollowingByUser(long userId);
    }
}