namespace Twitter.WebApplication.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Mvc;

    using Data;
    using Hubs;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.SignalR;
    using Models;
    using Twitter.Models;
    using UnitOfWork;

    public class TweetsController : BaseController
    {
        public TweetsController()
            :this(new TwitterData(new TwitterContext()))
        {
        }

        public TweetsController(ITwitterData data) 
            : base(data)
        {
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Authorize]
        public ActionResult AddTweet()
        {
            return this.View();
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult AddTweet(TweetInputModel tweet)
        {
            if (this.ModelState == null || !this.ModelState.IsValid)
            {
                return this.View(tweet);
            }

            var userId = this.User.Identity.GetUserId();
            var user = this.Data.Users.Find(userId);
            var newTweet = new Tweet
            {
                Author = user,
                AuthorId = userId,
                Text = tweet.Text,
                CreatedOn = DateTime.Now
            };
            try
            {
                this.Data.Tweets.Add(newTweet);
                foreach (var follower in user.Followers)
                {
                    this.Data.Notifications.Add(new Notification
                    {
                        Type = NotificationType.NewTweet,
                        User = follower,
                        Author = user,
                        AuthorId = user.Id,
                        Date = newTweet.CreatedOn,
                        IsNew = true,
                        UserId = follower.Id,
                        Content = string.Format("{0} added new tweet!", user.UserName)
                    });
                }
               
                this.Data.SaveChanges();

                var context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                var followrsNames = user.Followers.Select(f => f.UserName).ToList();
                context.Clients.Users(followrsNames).addTweet(newTweet.Id);
                context.Clients.Users(followrsNames).refreshNotificationsCount();
            }
            catch (Exception e)
            {
                //TODO: Make some real action
                return this.RedirectToAction("Index", "Home");
            }

            return this.RedirectToAction("Index", "Home");
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Authorize]
        public ActionResult LoadTweetPartial(int id)
        {
            var tweet = this.Data.Tweets.Find(id);
            var outputTweet = new TweetOutputModel
                {
                    Text = tweet.Text,
                    AuthorId = tweet.AuthorId,
                    Id = tweet.Id,
                    CreatedOn = tweet.CreatedOn,
                    FavoritesCount = tweet.UsersFavorite.Count,
                    AuthorName = tweet.Author.UserName,
                    RetweetsCount = tweet.UsersReTweets.Count
                };
            return this.PartialView("_Tweet", outputTweet);
        }
    }
}