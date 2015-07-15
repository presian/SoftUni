namespace Twitter.Data
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using Microsoft.AspNet.Identity.EntityFramework;
    
    using Migrations;
    using Models;

    public class TwitterContext : IdentityDbContext<User>
    {
        public TwitterContext()
            : base("TwitterDb", throwIfV1Schema: false)
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<TwitterContext, Configuration>());
        }


        //This method is never called but make whole project to work ;)
        public void FixEfProviderServicesProblem()
        {
            //The Entity Framework provider type 'System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer'
            //for the 'System.Data.SqlClient' ADO.NET provider could not be loaded. 
            //Make sure the provider assembly is available to the running application. 
            //See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.

            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }


        public static TwitterContext Create()
        {
            return new TwitterContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<User>()
                .HasMany<Tweet>(u => u.Tweets);

            modelBuilder.Entity<User>()
                .HasMany<Tweet>(u => u.FavoritTweets)
                .WithMany(t => t.UsersFavorite)
                .Map(x =>
                {
                    x.MapLeftKey("UserId");
                    x.MapRightKey("TweetId");
                    x.ToTable("UsersFavoritesTweets");
                });

            modelBuilder.Entity<User>()
                .HasMany<Tweet>(u => u.ReTweets)
                .WithMany(t => t.UsersReTweets)
                .Map(x =>
                {
                    x.MapLeftKey("UserId");
                    x.MapRightKey("TweetId");
                    x.ToTable("UsersReTweets");
                });

            modelBuilder.Entity<User>()
                .HasMany<User>(u => u.Followers)
                .WithMany(u=>u.Following)
                .Map(x =>
                {
                    x.MapLeftKey("UserId");
                    x.MapRightKey("FollworId");
                    x.ToTable("FollowersUsers");
                });

            modelBuilder.Entity<User>()
                .HasMany(u => u.Notifications)
                .WithRequired(n => n.Author);
                
            base.OnModelCreating(modelBuilder);
        }

        public IDbSet<Tweet> Tweets { get; set; }

        public IDbSet<Message> Messages { get; set; }

        public IDbSet<Replay> Replays { get; set; }

        public IDbSet<Notification> Notifications { get; set; }

        public IDbSet<ReportedTweet> ReportedTweets { get; set; }
    }
}
