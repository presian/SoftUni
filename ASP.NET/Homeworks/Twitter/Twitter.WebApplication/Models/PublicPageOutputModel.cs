namespace Twitter.WebApplication.Models
{
    using System.Collections.Generic;

    public class PublicPageOutputModel
    {
        public int TweetsCount { get; set; }

        public int FollowingCount { get; set; }

        public int FollowersCount { get; set; }

        public int FavoritesCount { get; set; }

        public ICollection<TweetOutputModel> Tweets { get; set; }
    }
}