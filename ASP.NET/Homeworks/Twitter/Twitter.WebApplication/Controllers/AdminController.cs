namespace Twitter.WebApplication.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data;
    using Models;
    using UnitOfWork;

    public class AdminController : BaseController
    {
        public AdminController()
            :this(new TwitterData(new TwitterContext()))
        {
        }

        public AdminController(ITwitterData data)
            :base(data)
        {
        }

        public ActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        public ActionResult Users()
        {
            var users = this.Data.Users.All()
                .Select(UserOutputModel.ViewModel);
            return this.View(users);
        }

        [HttpGet]
        public ActionResult Tweets()
        {
            return null;
        }

        [HttpGet]
        public ActionResult Notifications()
        {
            var noties = this.Data.Notifications.All().Select(NotificationOutputModel.ViewModel);
            return this.View(noties);
        }

        [HttpGet]
        public ActionResult Roles()
        {
            return null;
        }

        
    }
}