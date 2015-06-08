namespace Bookmarks.Web.Areas.User.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Data;

    using Models.DisplayModels;

    using UnitOfWork;

    using Web.Controllers;

    [Authorize]
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
            // Display first 6 
            var bookmarks = this.Data.Bookmarks.All()
                .OrderBy(b => b.Votes.Count)
                .Take(1)
                .Select(BookmarkDisplayModel.ViewModel);
            return this.View(bookmarks);
        }
    }
}