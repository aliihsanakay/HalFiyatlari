using System.Web.Mvc;

namespace HalFiyatlari.Api.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Hal Fiyatları";
            ViewBag.TitleEng = "Hal Deals";

            return View();
        }

        public ActionResult PrivacyPolicy()
        {
            ViewBag.Title = "Gizlilik Politikası";
            ViewBag.TitleEng = "Privacy Policy";

            return View();
        }

        public ActionResult TermsConditions()
        {
            ViewBag.Title = "Şartlar ve Koşullar";
            ViewBag.TitleEng = "Terms & Conditions";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Title = "İletişim";
            ViewBag.TitleEng = "Contact";

            return View();
        }
    }
}
