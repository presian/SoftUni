namespace Twitter.WebApplication.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Data;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Models;
    using UnitOfWork;
    using WebApplication.Controllers;
    using WebApplication.Models;

    public class NotificationsController : BaseController
    {
        public NotificationsController()
            : this(new TwitterData(new TwitterContext()))
        {
        }

        public NotificationsController(ITwitterData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return this.View(this.GetNotifications());
        }

        [HttpPost]
        public ActionResult GetPagedNotys([DataSourceRequest]DataSourceRequest request)
        {
            return this.Json(this.GetNotifications().ToDataSourceResult(request));
        }

        [HttpPost]
        public void UpdateNoty([DataSourceRequest]DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<NotificationOutputModel> notys)
        {
            // TODO: Make validations (username->unique, email->unique, ...)
            foreach (var noty in notys)
            {
                var notyToEdit = this.Data.Notifications.Find(noty.Id);
                var author = this.Data.Users.All().FirstOrDefault(u => u.UserName == noty.AuthorName);
                if (notyToEdit != null)
                {
                    notyToEdit.Author.UserName = noty.AuthorName;
                    notyToEdit.AuthorId = author.Id;
                    notyToEdit.Content = noty.Content;
                    notyToEdit.Date = noty.Date;
                    notyToEdit.Type = noty.Type;
                }
            }

            this.Data.SaveChanges();
        }


//        [HttpPost]
//        public ActionResult CreateNoty([DataSourceRequest]DataSourceRequest request,
//            [Bind(Prefix = "models")]IEnumerable<NotificationOutputModel> notys)
//        {
//            // TODO: Make validations
//            foreach (var noty in notys)
//            {
//                var author = this.Data.Users
//                    .All().FirstOrDefault(u => u.UserName == noty.AuthorName);
//                var newNoty = new Notification
//                {
//                    Author = author,
//                    AuthorId = author.Id,
//                    Content = noty.Content,
//                    Date = DateTime.Now,
//                    IsNew = true,
//                    Type = noty.Type,
//
//                    // This is not right but most easy way
//                    User = this.Data.Users.All().FirstOrDefault(u => u.Id != author.Id)
//                };
//
//                this.Data.Notifications.Add(newNoty);
//            }
//
//            this.Data.SaveChanges();
//            return this.Json(this.GetNotifications().ToDataSourceResult(request));
//        }

        [HttpPost]
        public void DeleteNoty([DataSourceRequest]DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<NotificationOutputModel> notys)
        {
            foreach (var noty in notys)
            {
                var notyToDelete = this.Data.Notifications.Find(noty.Id);
                this.Data.Notifications.Remove(notyToDelete);
            }

            this.Data.SaveChanges();
        }

        public IQueryable<NotificationOutputModel> GetNotifications()
        {
            return this.Data.Notifications.All()
                .Select(NotificationOutputModel.ViewModel);
        }
    }
}