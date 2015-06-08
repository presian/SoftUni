namespace Bookmarks.Web.Controllers
{
    using System.Web.Mvc;

    using Microsoft.Ajax.Utilities;

    using UnitOfWork;

    public abstract class BaseController : Controller
    {
        private IBookmarksData data;

        protected BaseController(IBookmarksData data)
        {
            this.data = data;
        }

        public IBookmarksData Data
        {
            get
            {
                return this.data;
            }

            set
            {
                this.data = value;
            }
        }

        public bool IsAdmin()
        {
            return this.User.IsInRole("Administrator");
        }
    }
}