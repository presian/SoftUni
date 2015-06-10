namespace Bookmarks.Web.Sevices
{
    using Controllers;
    using Data;
    using UnitOfWork;

    public class BookmarkServices : BaseController
    {
        public BookmarkServices() 
            : this(
            new BookmarksData(new BookmarksContext()))
        {
        }

        public BookmarkServices(IBookmarksData data)
            : base(data)
        {
        }

    }
}