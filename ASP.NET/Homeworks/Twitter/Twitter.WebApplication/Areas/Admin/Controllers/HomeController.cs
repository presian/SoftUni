namespace Twitter.WebApplication.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using Data;
    using UnitOfWork;
    using WebApplication.Controllers;

    public class HomeController : BaseController
    {
        public HomeController()
            : this(new TwitterData(new TwitterContext()))
        {
        }

        public HomeController(ITwitterData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return this.View();
        }
    }
}