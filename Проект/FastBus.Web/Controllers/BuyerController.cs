using System.Web.Mvc;

namespace FastBus.Web.Controllers
{
    public class BuyerController : BaseController
    {
        #region publicMethod

        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Schedule");
        }

        #endregion
    }
}
