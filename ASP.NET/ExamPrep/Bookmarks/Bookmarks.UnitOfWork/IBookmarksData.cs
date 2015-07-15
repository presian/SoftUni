namespace Bookmarks.UnitOfWork
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using Repository;

    public interface IBookmarksData
    {
        IRepository<User> Users { get; }

        IRepository<IdentityRole> Roles { get; }

        IRepository<Bookmark> Bookmarks { get; }

        IRepository<Category> Categories { get; }

        IRepository<Comment> Comments { get; }

        IRepository<Vote> Votes { get; }

        IUserStore<User> UserStore { get; }

        void SaveChanges();
    }
}
