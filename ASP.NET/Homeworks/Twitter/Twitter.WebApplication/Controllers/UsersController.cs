namespace Twitter.WebApplication.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;
    using Data;
    using Microsoft.AspNet.Identity;
    using Models;
    using PagedList;
    using UnitOfWork;
    using Constants = Utilities.Constants;

    public class UsersController : BaseController
    {
        public UsersController() 
            : base(new TwitterData(new TwitterContext()))
        {
        }

        public UsersController(ITwitterData data) 
            : base(data)
        {
        }

        [Authorize]
        [HttpGet]
        public ActionResult Index(int? page)
        {
            var userId = this.User.Identity.GetUserId();
            var currentUser = this.Data.Users.Find(userId);
            var followings = currentUser.Following;
            var tweets = currentUser.Following
                .SelectMany(f => f.Tweets)
                .OrderByDescending(t=>t.CreatedOn)
                .Select(t => new TweetOutputModel
                {
                    AuthorId = t.AuthorId,
                    CreatedOn = t.CreatedOn,
                    Text = t.Text,
                    AuthorName = t.Author.UserName,
                    FavoritesCount = t.UsersFavorite.Count,
                    RetweetsCount = t.UsersReTweets.Count
                });

            int pageNumber = (page ?? 1);

            return this.View("Index", tweets.ToPagedList(pageNumber, Constants.PageSize));
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddNewTweet()
        {
            return this.RedirectToAction("AddTweet", "Tweets");
        }

        [HttpGet]
        public ActionResult PublicPage(string username)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(username))
            {
                var currentUserName = this.User.Identity.GetUserName();
                if (!string.IsNullOrEmpty(currentUserName))
                {
                    return this.RedirectToAction("Index");
                }

                return this.RedirectToAction("Index", "Home");
            }

            var searchedUser = this.Data.Users.All()
                .Include(u=>u.Following)
                .Include(u => u.Followers)
                .Include(u=>u.Tweets)
                .Include(u=>u.FavoritTweets)
                .FirstOrDefault(u => u.UserName == username);
            if (searchedUser == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            var model = new PublicPageOutputModel
            {
                TweetsCount = searchedUser.Tweets.Count,
                FavoritesCount = searchedUser.FavoritTweets.Count,
                FollowersCount = searchedUser.Followers.Count,
                FollowingCount = searchedUser.Following.Count,
                Tweets = searchedUser.Tweets.Select(t => new TweetOutputModel
                {
                    AuthorId = t.AuthorId,
                    Text = t.Text,
                    AuthorName = t.Author.UserName,
                    Id = t.Id,
                    FavoritesCount = t.UsersFavorite.Count,
                    CreatedOn = t.CreatedOn,
                    RetweetsCount = t.UsersReTweets.Count
                }).ToList()
            };

            return this.View(model);
        }

        [Authorize]
        [HttpGet]
        public ActionResult EditProfile()
        {
            return this.View();
        }
    }
}