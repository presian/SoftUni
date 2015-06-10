namespace Bookmarks.Web.Controllers
{
    using System.Web.Mvc;
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

            return this.View();
        }

        public ActionResult SeeAll(int? page)
        {
            if (this.IsAdmin())
            {
                return this.RedirectToAction("SeeAll", "Home", new { area = "Admin" });
            }

            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("SeeAll", "Home", new { area = "User" });
            }

            return this.View();
        }
    }
}