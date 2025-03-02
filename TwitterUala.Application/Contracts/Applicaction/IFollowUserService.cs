namespace TwitterUala.Application.Contracts.Applicaction
{
    public interface IFollowUserService
    {
        void FollowUser(long userId, long userToFollowId);
    }
}