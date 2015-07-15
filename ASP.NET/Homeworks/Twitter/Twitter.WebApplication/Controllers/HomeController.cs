namespace Twitter.WebApplication.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;

    using PagedList;

    using Data;
    using Microsoft.AspNet.Identity;
    using Models;
    using UnitOfWork;
    using Constants = Utilities.Constants;


    public class HomeController : BaseController
    {
        public HomeController()
            :this(new TwitterData(new TwitterContext()))
        {
        }

        public HomeController(ITwitterData data)
            :base(data)
        {
        }


        [HttpGet]
        public ActionResult Index(int? page)
        {
            var userId = this.User.Identity.GetUserId();
            if (userId != null)
            {
                return this.RedirectToAction("Index", "Users");
            }

            var tweets = this.Data.Tweets.All()
                .Include(t => t.Author)
                .OrderByDescending(t => t.CreatedOn)
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

        [HttpGet]
        public ActionResult Users(int? page)
        {
            var usersData = this.Data.Users.All()
                .Include(u => u.Tweets)
                .Include(u => u.FavoritTweets)
                .Include(u => u.ReTweets)
                .Include(u => u.Following)
                .Include(u => u.Followers)
                .Select(o => new UserListOutputModel
                {
                    Username = o.UserName,
                    Id = o.Id,
                    TweetsCount = o.Tweets.Count,
                    FollowingCount = o.Following.Count,
                    FollowrsCount = o.Followers.Count,
                    ReTweetCount = o.ReTweets.Count,
                    ReplayCount = o.Replays.Count,
                    FavoritesCount = o.FavoritTweets.Count
                })
                .ToList();
            int pageNumber = (page ?? 1);
            return this.View("Users", usersData.ToPagedList(pageNumber, Constants.PageSize));
        }
    }
}