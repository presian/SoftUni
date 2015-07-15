namespace Twitter.WebApplication.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;
    using Data;
    using Microsoft.AspNet.Identity;
    using Models;
    using UnitOfWork;

    [Authorize]
    public class NotificationsController : BaseController
    {
        public NotificationsController()
            : this(new TwitterData( new TwitterContext()))
        {
        }

        public NotificationsController(ITwitterData data) 
            : base(data)
        {
        }

        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            var userId = this.User.Identity.GetUserId();
            var notifications = this.Data.Notifications.All()
                .Include(n=>n.Author)
                .Where(n => n.UserId == userId)
                .OrderByDescending(n=>n.Date)
                .Select(n => new NotificationOutputModel
                {
                    Type = n.Type,
                    Date = n.Date,
                    AuthorName = n.Author.UserName,
                    Content = n.Content
                })
                .ToList();

            foreach (var noty in this.Data.Notifications
                .All()
                .Where(n => n.UserId == userId))
            {
                noty.IsNew = false;
                this.Data.Notifications.Update(noty);
            }

            this.Data.SaveChanges();
            
            return this.View(notifications);
        }

        [HttpGet]
        [Authorize]
        public string GetNotificationsCount()
        {
            var userId = this.User.Identity.GetUserId();
            var count = this.Data.Notifications.All()
                .Count(n => n.UserId == userId && n.IsNew == true);
            return count > 0 ? count.ToString() : "";
        }
    }
}