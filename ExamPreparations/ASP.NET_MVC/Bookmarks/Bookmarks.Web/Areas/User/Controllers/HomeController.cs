namespace Bookmarks.Web.Areas.User.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using Bookmarks.Models;

    using Extensions;

    using Microsoft.AspNet.Identity;

    using Models.DisplayModels;
    using Models.EditorModels;

    public class HomeController : BaseUserAreaController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        public ActionResult SeeAll(int? page)
        {
            return this.View();
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var bookmark = this.Data.Bookmarks.Find(id);
            if (bookmark == null)
            {
                this.AddNotification("This bookmark has not single view page!", NotificationType.ERROR);
                return this.RedirectToAction("Index");
            }

            return this.View(DetailsDisplayModel.CreateFromBookmark(bookmark));
        }

        [HttpGet]
        public ActionResult AddComment()
        {
            return this.PartialView("EditorTemplates/CommentEditorModel");
        }

        [HttpPost]
        public ActionResult AddComment(int id, CommentEditorModel newComment)
        {
            var bookmark = this.Data.Bookmarks.Find(id);
            if (bookmark == null)
            {
                this.AddNotification("This bookmark is unavaible and you cannot add new comments!", NotificationType.ERROR);
                return this.RedirectToAction("SeeAll", "Home", new{ area = "User"});
            }

            if (!this.ModelState.IsValid)
            {
                this.AddNotification("Text field is requred!", NotificationType.ERROR);
                return this.RedirectToAction("Details", "Home", new { area = "User", id = id });
            }

            var userId = this.User.Identity.GetUserId();
            var user = this.Data.Users.Find(userId);

            var comment = new Comment
            {
                Text = newComment.Text,
                Author = user,
                AuthorId = userId,
                Bookmark = bookmark,
                BookmarkId = id
            };

            this.Data.Comments.Add(comment);

            this.Data.SaveChanges();

            return this.PartialView("DisplayTemplates/CommentDisplayModel", CommentDisplayModel.CreateFromComment(comment));
        }

        [HttpPost]
        public ActionResult AddVote(int id)
        {
            var bookmark = this.Data.Bookmarks.Find(id);
            if (bookmark == null)
            {
                this.AddNotification("This bookmark is unavaible and you cannot add new comments!", NotificationType.ERROR);
                return this.RedirectToAction("SeeAll", "Home", new { area = "User" });
            }

            var userId = this.User.Identity.GetUserId();
            var user = this.Data.Users.Find(userId);

            if (user == null)
            {
                this.AddNotification("You are not in our system, please loged in!", NotificationType.ERROR);
                return this.RedirectToAction("Login", "Account", new { area = "" });
            }

            var vote = new Vote
            {
                User = user,
                UserId = userId,
                Bookmark = bookmark,
                BookmarkId = id
            };

            this.Data.Votes.Add(vote);
            this.Data.SaveChanges();

            return this.Content(bookmark.Votes.Count.ToString());
        }

        [HttpGet]
        public ActionResult AddBookmark()
        {
            this.ViewBag.Categories = this.Data.Categories.All().Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            });

            return this.View();
        }

        [HttpPost]
        public ActionResult AddBookmark(BookmarkEditorModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewBag.Categories = this.Data.Categories.All().Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                });

                return this.View(model);
            }

            var userId = this.User.Identity.GetUserId();
            var user = this.Data.Users.Find(userId);

            if (user == null)
            {
                this.AddNotification("You must be loged in for submitting new bookmark", NotificationType.ERROR);
                this.RedirectToAction("Login", "Account");
            }

            var category = this.Data.Categories.Find(model.CategoryId);
            if (category == null)
            {
                this.AddNotification("This category is unvaible now please select another", NotificationType.ERROR);

                this.ViewBag.Categories = this.Data.Categories.All().Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                });

                return this.View(model);
            }


            var newBookmark = new Bookmark
            {
                User = user,
                UserId = userId,
                Category = category,
                CategoryId = model.CategoryId,
                Votes = new List<Vote>(),
                Title = model.Title,
                Comments = new List<Comment>(),
                Description = model.Description,
                Url = model.Url
            };

            this.Data.Bookmarks.Add(newBookmark);
            this.Data.SaveChanges();

            return this.RedirectToAction("Index", "Home", new {area = "User"});
        }

    }
}