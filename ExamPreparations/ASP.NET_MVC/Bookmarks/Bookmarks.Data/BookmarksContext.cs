namespace Bookmarks.Data
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Migrations;
    using Models;

    public class BookmarksContext : IdentityDbContext<User>
    {
        public BookmarksContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<BookmarksContext, 
                    BookmarksMigrationConfiguration>());
        }

        public static BookmarksContext Create()
        {
            return new BookmarksContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<User>()
                .HasMany(u => u.Votes);

            modelBuilder.Entity<Vote>()
                .HasRequired(v => v.User)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vote>()
                .HasRequired(v => v.Bookmark)
                .WithMany()
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
//
//            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
//            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
//            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.UserId, r.RoleId });
        }

        public IDbSet<Bookmark> Bookmarks { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public IDbSet<Category> Categories { get; set; }
    }
}
