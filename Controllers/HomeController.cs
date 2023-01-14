using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Course_Project_TP_6.Models;

namespace Course_Project_TP_6.Controllers
{
    public class HomeController : Controller
    {
        private passportofficeEntities db = new passportofficeEntities();

        public ActionResult Index()
        {
            Users currentUser = db.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();
            ViewBag.IsAdmin = currentUser != null && currentUser.Role_Id == 2;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}