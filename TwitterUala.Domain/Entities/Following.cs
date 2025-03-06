namespace TwitterUala.Domain.Entities
{
    public partial class Following
    {
        //public long IdFollowing { get; set; }
        public long UserId { get; set; }
        public long UsersToFollowId { get; set; }
        public virtual HashSet<Tweet> TweetsUser { get; set; }
    }
}
