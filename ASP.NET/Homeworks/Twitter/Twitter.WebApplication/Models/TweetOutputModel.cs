namespace Twitter.WebApplication.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;
    using Twitter.Models;

    public class TweetOutputModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(160)]
        public string Text { get; set; }

        public string AuthorId { get; set; }

        public string AuthorName { get; set; }

        public DateTime CreatedOn { get; set; }

        public int FavoritesCount { get; set; }

        public int RetweetsCount { get; set; }

        public static Expression<Func<Tweet, TweetOutputModel>> ViewModel
        {
            get
            {
                return t => new TweetOutputModel
                {
                    Id = t.Id,
                    AuthorName = t.Author.UserName,
                    AuthorId = t.AuthorId,
                    CreatedOn = t.CreatedOn,
                    FavoritesCount = t.UsersFavorite.Count,
                    RetweetsCount = t.UsersReTweets.Count,
                    Text = t.Text
                };
            }
        }
    }
}