namespace Twitter.WebApplication.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Data;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Models;
    using Twitter.Models;
    using UnitOfWork;
    using WebApplication.Controllers;
    using WebApplication.Models;

    public class TweetsController : BaseController
    {
        public TweetsController()
            : this(new TwitterData(new TwitterContext()))
        {
        }

        public TweetsController(ITwitterData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return this.View(this.GetTweets());
        }

        [HttpPost]
        public ActionResult GetPagedTweets([DataSourceRequest]DataSourceRequest request)
        {
            return this.Json(this.GetTweets().ToDataSourceResult(request));
        }

        [HttpPost]
        public void UpdateTweet([DataSourceRequest]DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<TweetOutputModel> tweets)
        {
            // TODO: Make validations (username->unique, email->unique, ...)
            foreach (var tweet in tweets)
            {
                var tweetToEdit = this.Data.Tweets.Find(tweet.Id);
                if (tweetToEdit != null)
                {
                    tweetToEdit.Author.UserName = tweet.AuthorName;
                    tweetToEdit.AuthorId = tweet.AuthorId;
                    tweetToEdit.CreatedOn = tweet.CreatedOn;
                    tweetToEdit.Text = tweet.Text;
                }
            }

            this.Data.SaveChanges();
        }

        [HttpPost]
        public ActionResult CreateTweet([DataSourceRequest]DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<TweetOutputModel> tweets)
        {
            // TODO: Make validations
            foreach (var tweet in tweets)
            {
                var author = this.Data.Users
                    .All().FirstOrDefault(u => u.UserName == tweet.AuthorName);
                var newTweet = new Tweet
                {
                    AuthorId = author.Id,
                    Text = tweet.Text,
                    Author = author,
                    CreatedOn = DateTime.Now,
                };

                this.Data.Tweets.Add(newTweet);
            }

            this.Data.SaveChanges();
            return this.Json(this.GetTweets().ToDataSourceResult(request));
        }

        [HttpPost]
        public void DeleteTweet([DataSourceRequest]DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<TweetOutputModel> tweets)
        {
            foreach (var tweet in tweets)
            {
                var tweetToDelete = this.Data.Tweets.Find(tweet.Id);
                this.Data.Tweets.Remove(tweetToDelete);
            }

            this.Data.SaveChanges();
        }

        public IQueryable<TweetOutputModel> GetTweets()
        {
            return this.Data.Tweets.All()
                .Select(TweetOutputModel.ViewModel);
        }
    }
}