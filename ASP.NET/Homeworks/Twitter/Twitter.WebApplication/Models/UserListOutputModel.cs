namespace Twitter.WebApplication.Models
{
    public class UserListOutputModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public int TweetsCount { get; set; }

        public int FollowrsCount { get; set; }

        public int FollowingCount { get; set; }

        public int ReplayCount { get; set; }

        public int ReTweetCount { get; set; }
        public int FavoritesCount { get; set; }
    }
}