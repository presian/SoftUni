﻿namespace Bookmarks.Web.Areas.User.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Extensions;

    using Models.DisplayModels;

    public class HomeController : BaseUserAreaController
    {
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult SeeAll(int? page)
        {
            return this.View();
        }

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
    }
}