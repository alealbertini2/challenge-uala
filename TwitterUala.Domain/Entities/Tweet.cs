namespace TwitterUala.Domain.Entities
{
    public partial class Tweet
    {
        public long IdTweet { get; set; }
        public long UserId { get; set; }
        public string TweetMessage { get; set; }
        public DateTime TweetPosted { get; set; }
        public Following Following {  get; set; }
    }
}
