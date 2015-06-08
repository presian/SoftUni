namespace Bookmarks.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Areas.User.Models.DisplayModels;
    using Data;
    using UnitOfWork;

    [AllowAnonymous]
    public class HomeController : BaseController
    {
        public HomeController() 
            : this(new BookmarksData(new BookmarksContext()))
        {
        }

        public HomeController(IBookmarksData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            if (this.IsAdmin())
            {
                return this.RedirectToAction("Index", "Home", new { area = "Admin" });
            }

            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Home", new {area = "User"});
            }

            // Display first 6 
            var bookmarks = this.Data.Bookmarks.All()
                .OrderByDescending(b => b.Votes.Count)
                .ThenBy(b => b.Id)
                .Take(6)
                .Select(BookmarkDisplayModel.ViewModel);

            this.ViewBag.Area = "";

            return this.View(bookmarks);
        }

        public ActionResult SeeAll()
        {
            if (this.IsAdmin())
            {
                return this.RedirectToAction("SeeAll", "Home", new { area = "Admin" });
            }

            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("SeeAll", "Home", new { area = "User" });
            }

            var bookmarks = this.Data.Bookmarks.All()
                .OrderByDescending(b => b.Votes.Count)
                .ThenBy(b => b.Id)
                .Select(BookmarkDisplayModel.ViewModel);

            this.ViewBag.Area = "";

            return this.View(bookmarks);
        }
    }
}