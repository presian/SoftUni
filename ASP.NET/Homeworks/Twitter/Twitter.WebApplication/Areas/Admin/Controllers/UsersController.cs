namespace Twitter.WebApplication.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Data;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Models;
    using Twitter.Models;
    using UnitOfWork;
    using WebApplication.Controllers;
    using WebApplication.Models;

    public class UsersController : BaseController
    {
        public UsersController()
            : this(new TwitterData(new TwitterContext()))
        {
        }

        public UsersController(ITwitterData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {

            return this.View(this.GetUsers());
        }

        [HttpPost]
        public ActionResult GetPagedUsers([DataSourceRequest]DataSourceRequest request)
        {
            return this.Json(this.GetUsers().ToDataSourceResult(request));
        }

        [HttpPost]
        public void UpdateUser([DataSourceRequest]DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<UserOutputModel> users)
        {
            // TODO: Make validations (username->unique, email->unique, ...)
            foreach (var user in users)
            {
                var userToEdit = this.Data.Users.Find(user.Id);
                if (userToEdit != null)
                {
                    userToEdit.FirstName = user.FirstName;
                    userToEdit.LastName = user.LastName;
                    userToEdit.Email = user.Email;
                    userToEdit.UserName = user.Username;
                }
            }

            this.Data.SaveChanges();
        }

        [HttpPost]
        public ActionResult CreateUser([DataSourceRequest]DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<UserOutputModel> users)
        {
            var userManager = this.HttpContext.GetOwinContext().
                    GetUserManager<ApplicationUserManager>();

            // TODO: Make validations (username->unique, email->unique, ...)
            foreach (var user in users)
            {
                var newUser = new User
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.Username
                };
                
                userManager.Create(newUser, newUser.UserName + DateTime.Now.Year);
            }

            return this.Json(this.GetUsers().ToDataSourceResult(request));
        }

        [HttpPost]
        public void DeleteUser([DataSourceRequest]DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<UserOutputModel> users)
        {
            foreach (var user in users)
            {
                var userToDelete = this.Data.Users.Find(user.Id);
                this.Data.Users.Remove(userToDelete);
            }

            this.Data.SaveChanges();
        }

        public IQueryable<UserOutputModel> GetUsers()
        {
            return this.Data.Users.All()
                .Select(UserOutputModel.ViewModel);
        }
    }
}