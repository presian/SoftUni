namespace Bookmarks.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Models;
    using Data;
    using PagedList;
    using Sevices;
    using UnitOfWork;

    public class PublicController : BaseController
    {
         public PublicController() 
            : base(new BookmarksData(new BookmarksContext()))
        {
        }

        public PublicController(IBookmarksData data)
            : base(data)
        {
        }


        public ActionResult Index()
        {
            var bookmarks = this.Data.Bookmarks.All()
                .OrderByDescending(b => b.Votes.Count)
                .ThenBy(b => b.Id)
                .Take(6)
                .Select(BookmarkDisplayModel.ViewModel);

            return this.PartialView("_Home", bookmarks);
        }

        public ActionResult SeeAll(int? page)
        {
            var bookmarks = this.Data.Bookmarks.All()
                .OrderByDescending(b => b.Votes.Count)
                .ThenBy(b => b.Id)
                .Select(BookmarkDisplayModel.ViewModel);

            int pageSize = Utilities.BookmarksPageSize;
            int pageNumber = (page ?? 1);

            return this.PartialView("_SeeAll", bookmarks.ToPagedList(pageNumber, pageSize));
        }
    }
}