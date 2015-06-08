namespace Bookmarks.Web.Areas.User.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Models.DisplayModels;

    public class HomeController : BaseUserAreaController
    {
        public ActionResult Index()
        {
            // Display first 6 
            var bookmarks = this.Data.Bookmarks.All()
                .OrderByDescending(b => b.Votes.Count)
                .ThenBy(b => b.Id)
                .Take(6)
                .Select(BookmarkDisplayModel.ViewModel);

            this.ViewBag.Area = "User";

            return this.View(bookmarks);
        }

        public ActionResult SeeAll()
        {
            var bookmarks = this.Data.Bookmarks.All()
                .OrderByDescending(b => b.Votes.Count)
                .ThenBy(b => b.Id)
                .Select(BookmarkDisplayModel.ViewModel);

            this.ViewBag.Area = "User";

            return this.View(bookmarks);
        }

        public ActionResult Details()
        {
            return this.View();
        }
    }


}