using TwitterUala.Domain.Entities;

namespace TwitterUala.Application.Contracts.Applicaction
{
    public interface ITweetsFromFollowingByUserService
    {
        Task<List<Tweet>> TimelineByUserIdAsync(long userId);
    }
}