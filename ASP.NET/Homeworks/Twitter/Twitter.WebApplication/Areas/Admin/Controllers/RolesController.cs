namespace Twitter.WebApplication.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Data;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using UnitOfWork;
    using WebApplication.Controllers;

    public class RolesController : BaseController
    {
        public RolesController()
            : this(new TwitterData(new TwitterContext()))
        {
        }

        public RolesController(ITwitterData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            var roles = this.GetRoles();

            return this.View(roles);
        }

        [HttpPost]
        public ActionResult GetPagedRoles([DataSourceRequest]DataSourceRequest request)
        {
            return this.Json(this.GetRoles().ToDataSourceResult(request));
        }

        [HttpPost]
        public void UpdateRole([DataSourceRequest]DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<RoleEditModel> roles)
        {
            // TODO: Make validations
            foreach (var role in roles)
            {
                var roleToEdit = this.Data.Roles.Find(role.Id);
                if (roleToEdit != null)
                {
                    roleToEdit.Name = role.Name;
                }
            }

            this.Data.SaveChanges();
        }

        [HttpPost]
        public ActionResult CreateRole([DataSourceRequest]DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<RoleEditModel> roles)
        {
            // TODO: Make validations
            foreach (var role in roles)
            {
                this.Data.Roles.Add(new IdentityRole(role.Name));
            }

            this.Data.SaveChanges();
            return this.Json(this.GetRoles().ToDataSourceResult(request));
        }

        [HttpPost]
        public void DeleteRole([DataSourceRequest]DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<RoleEditModel> roles)
        {
            foreach (var role in roles)
            {
                this.Data.Roles.Remove(role.Id);
            }

            this.Data.SaveChanges();
        }

        public IQueryable<RoleEditModel> GetRoles()
        {
            return this.Data.Roles.All().Select(r => new RoleEditModel
            {
                Id = r.Id,
                Name = r.Name
            });
        }
    }
}