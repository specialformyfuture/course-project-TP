using Course_Project_TP_6.DAO;
using Course_Project_TP_6.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Course_Project_TP_6.Controllers
{
    public class LoginController : Controller
    {
        
        private passportofficeEntities db = new passportofficeEntities();

        // GET: Login
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Users users)
        {
            if (!ModelState.IsValid)
            {
                return View(users);
            }

            users.Role_Id = 1;
            db.Users.Add(users);
            try {
                db.SaveChanges();
                return RedirectToAction("Index", "Home"); 
            } catch (Exception ex) {
                Debug.WriteLine($"<Register()> Ошибка при добавлении записи: {ex}");
                return RedirectToAction("Error", "Shared");
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Email, string Password)
        {
            var hasErrors = false;

            if (!Email.Contains("@"))
            {
                ModelState.AddModelError("Email", "Некорректный email");
                hasErrors = true;
            }

            if (hasErrors) return View();

            Users user = db.Users
                .Where(x => x.Email == Email && x.Password == Password)
                .FirstOrDefault();
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(Email, true);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}