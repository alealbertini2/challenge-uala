namespace TwitterUala.Application.Contracts.Applicaction
{
    public interface IFollowUserService
    {
        Task FollowUser(long userId, long userToFollowId);
    }
}