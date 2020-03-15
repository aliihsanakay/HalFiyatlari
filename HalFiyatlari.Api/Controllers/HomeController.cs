using HalFiyatlari.Api.Models.Select;
using System.Collections.Generic;
using HalFiyatlari.Business;
using System.Web.Mvc;
using System.Data;

namespace HalFiyatlari.Api.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Hal Fiyatları";
            ViewBag.TitleEng = "Hal Deals";



            ViewBag.Customer = Customer.GetCustomers();
            return View();
        }

        [HttpGet]
        public ActionResult Search()
        {
            ViewBag.Title = "Arama";
            ViewBag.TitleEng = "Search";

            return View();
        }

        //[HttpPost]
        //public ActionResult Search(SearchDataModel searchDataModel)
        //{
        //    ViewBag.Title = "Arama";
        //    ViewBag.TitleEng = "Search";

        //    return View(searchDataModel);
        //}

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
