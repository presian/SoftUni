namespace Twitter.UnitOfWork
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Repositories;
    using Models;

    public interface ITwitterData
    {
        IRepository<User> Users { get; }

        IRepository<IdentityRole> Roles { get; }

        IRepository<Tweet> Tweets { get; }

        IRepository<Message> Messages { get; }

        IRepository<Replay> Replays { get; }

        IRepository<Notification> Notifications { get; }

        IRepository<ReportedTweet> ReportedTweets { get; }

        IUserStore<User> UserStore { get; }

        void SaveChanges();
    }
}
