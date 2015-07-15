namespace Twitter.UnitOfWork
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using Repositories;
    using Models;
    using Data;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class TwitterData : ITwitterData
    {
        private readonly DbContext dbContext;

        private readonly IDictionary<Type, object> repositories;

        private IUserStore<User> userStore;

        public TwitterData()
            : this(new TwitterContext())
        {
        }

        public TwitterData(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<User> Users
        {
            get { return this.GetRepository<User>(); }
        }

        public IRepository<IdentityRole> Roles
        {
            get { return this.GetRepository<IdentityRole>(); }
        }

        public IRepository<Tweet> Tweets
        {
            get { return this.GetRepository<Tweet>(); }
        }

        public IRepository<Message> Messages
        {
            get { return this.GetRepository<Message>(); }
        }

        public IRepository<Notification> Notifications
        {
            get { return this.GetRepository<Notification>(); }
        }

        public IRepository<Replay> Replays
        {
            get { return this.GetRepository<Replay>(); }
        }

        public IRepository<ReportedTweet> ReportedTweets
        {
            get { return this.GetRepository<ReportedTweet>(); }
        }

        public IUserStore<User> UserStore
        {
            get
            {
                if (this.userStore == null)
                {
                    this.userStore = new UserStore<User>(this.dbContext);
                }
                return this.userStore;
            }
        }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(GenericRepository<T>);
                this.repositories.Add(typeof(T),
                    Activator.CreateInstance(type, this.dbContext));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }
    }
}
