namespace Bookmarks.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using Data;
    using UnitOfWork;
    using Web.Controllers;

    [Authorize(Roles = "Administrator")]
    public abstract class BaseAdminController : BaseController
    {
        public BaseAdminController()
            : this(new BookmarksData(new BookmarksContext()))
        {
        }

        public BaseAdminController(IBookmarksData data)
            : base(data)
        {
        }
    }
}