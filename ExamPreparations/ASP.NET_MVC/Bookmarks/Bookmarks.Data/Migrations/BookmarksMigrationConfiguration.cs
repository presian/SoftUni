namespace Bookmarks.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    internal sealed class BookmarksMigrationConfiguration : DbMigrationsConfiguration<BookmarksContext>
    {
        public BookmarksMigrationConfiguration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(BookmarksContext context)
        {
            if (!context.Roles.Any())
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var roleAdmin = new IdentityRole { Name = "Administrator" };
                var roleUser = new IdentityRole { Name = "RegularUser" };

                manager.Create(roleAdmin);
                manager.Create(roleUser);
            }

            if (!context.Users.Any())
            {
                var store = new UserStore<User>(context);
                var manager = new UserManager<User>(store)
                {
                    PasswordValidator = new PasswordValidator
                    {
                        RequiredLength = 1,
                        RequireNonLetterOrDigit = false,
                        RequireDigit = false,
                        RequireLowercase = false,
                        RequireUppercase = false,
                    }
                };
                var firstUser = new User { Email = "pesho@abv.bg", UserName = "pesho@abv.bg" };
                var secondUser = new User { Email = "gosho@abv.bg", UserName = "gosho@abv.bg" };
                var admin = new User { Email = "admin@abv.bg", UserName = "admin@abv.bg" };

                var res1 = manager.Create(firstUser, firstUser.UserName);
                var res2 = manager.Create(secondUser, secondUser.UserName);
                var res3 = manager.Create(admin, admin.UserName);

                manager.AddToRole(firstUser.Id, "RegularUser");
                manager.AddToRole(secondUser.Id, "RegularUser");
                manager.AddToRole(admin.Id, "RegularUser");
                manager.AddToRole(admin.Id, "Administrator");
            }

            if (!context.Categories.Any())
            {
                context.Categories.Add(new Category
                {
                    Name = "PHP"
                });

                context.Categories.Add(new Category
                {
                    Name = "C#"
                });

                context.Categories.Add(new Category
                {
                    Name = "JavaScript"
                });

                context.SaveChanges();
            }

            if (!context.Bookmarks.Any())
            {
                var pesho = context.Users.FirstOrDefault(u => u.Email == "pesho@abv.bg");
                var gosho = context.Users.FirstOrDefault(u => u.Email == "gosho@abv.bg");
                context.Bookmarks.Add(new Bookmark
                {
                    Category = context.Categories.FirstOrDefault(c => c.Name == "C#"),
                    User = pesho,
                    Comments = new List<Comment>
                    {
                        new Comment
                        {
                            Author = gosho,
                            Text = "AAAAeeee beee"
                        },
                        new Comment
                        {
                            Author = pesho,
                            Text = "E neeeee beee"
                        }

                    },
                    Title = "New in C#",
                    Url = "www.msdn.com",
                    Votes = new List<Vote>
                    {
                        new Vote
                        {
                            User = pesho
                        }, 
                        new Vote
                        {
                           User = gosho 
                        }
                    }
                });

                context.Bookmarks.Add(new Bookmark
                {
                    Category = context.Categories.FirstOrDefault(c => c.Name == "JavaScript"),
                    User = gosho,
                    Comments = new List<Comment>
                    {
                        new Comment
                        {
                            Author = gosho,
                            Text = "AAAAeeee beee"
                        },
                        new Comment
                        {
                            Author = pesho,
                            Text = "E neeeee beee"
                        }

                    },
                    Title = "New in JS",
                    Url = "www.google.com",
                    Votes = new List<Vote>
                    {
                        new Vote
                        {
                            User = pesho
                        }, 
                        new Vote
                        {
                           User = gosho 
                        }
                    }
                });

                context.SaveChanges();
            }

        }
    }
}
