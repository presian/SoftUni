namespace Bookmarks.Web.Areas.User.Controllers
{
    using System.Web.Mvc;
    using Data;
    using UnitOfWork;
    using Web.Controllers;

    [Authorize(Roles = "RegularUser")]
    public abstract class BaseUserAreaController : BaseController
    {
        public BaseUserAreaController()
            : this(new BookmarksData(new BookmarksContext()))
        {
        }

        public BaseUserAreaController(IBookmarksData data) 
            : base(data)
        {
        }
    }
}