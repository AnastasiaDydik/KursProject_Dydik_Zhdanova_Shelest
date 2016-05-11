using Kurs.Providers;
using System.Web.Mvc;

namespace Kurs.Controllers
{
    public class HomeController : Controller
    {
        //readonly ITestService TestService;
        /*public HomeController(ITestService testService)
        {
            TestService = testService;
        }*/
        public ActionResult Index()
        {
          //  ViewBag.Title = TestService.GetString("Привет!!");

            return View();
        }
    }
}
