namespace Twitter.WebApplication.Controllers
{
    using System.Web.Mvc;
    using UnitOfWork;

    public abstract class BaseController : Controller
    {
        private ITwitterData data;

        public BaseController(ITwitterData data)
        {
            this.data = data;
        }

        protected ITwitterData Data
        {
            get
            {
                return this.data;
            }
        }
    }
}
