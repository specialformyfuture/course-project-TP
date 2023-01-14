using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Course_Project_TP_6.Models;

namespace Course_Project_TP_6.Controllers
{
    [Authorize]
    public class RegistrationsController : Controller
    {
        private passportofficeEntities db = new passportofficeEntities();

        // GET: Registrations
        public ActionResult Index()
        {
            Users currentUser = db.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();
            bool isAdmin = currentUser.Role_Id == 2;
            if (!isAdmin)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized); 
            }

            var registration = db.Registration.Include(r => r.Passport);
            return View(registration.ToList());
        }

        // GET: Registrations/Details/5
        public ActionResult Details(int? id)
        {
            Users currentUser = db.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();
            bool isAdmin = currentUser.Role_Id == 2;
            if (!isAdmin)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized); 
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = db.Registration.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        // GET: Registrations/Create
        public ActionResult Create()
        {
            Users currentUser = db.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();
            bool isAdmin = currentUser.Role_Id == 2;
            if (!isAdmin)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized); 
            }

            ViewBag.Passport_Id = new SelectList(db.Passport, "Passport_Id", "IssuedBy");
            return View();
        }

        // POST: Registrations/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Registration_Id,Passport_Id,RegistrationDate,Region,City,District,Street,Building,Apartament")] Registration registration)
        {
            Users currentUser = db.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();
            bool isAdmin = currentUser.Role_Id == 2;
            if (!isAdmin)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized); 
            }

            var passport = db.Passport.Where(x => x.Number == registration.Passport_Id).FirstOrDefault();
            if (passport == null)
            {
                registration.Passport_Id = 0;
                return View(registration);
            }

            registration.Passport_Id = passport.Passport_Id;
            if (ModelState.IsValid)
            {
                db.Registration.Add(registration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Passport_Id = new SelectList(db.Passport, "Passport_Id", "IssuedBy", registration.Passport_Id);
            return View(registration);
        }

        // GET: Registrations/Edit/5
        public ActionResult Edit(int? id)
        {
            Users currentUser = db.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();
            bool isAdmin = currentUser.Role_Id == 2;
            if (!isAdmin)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized); 
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = db.Registration.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            ViewBag.Passport_Id = new SelectList(db.Passport, "Passport_Id", "IssuedBy", registration.Passport_Id);
            return View(registration);
        }

        // POST: Registrations/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Registration_Id,Passport_Id,RegistrationDate,Region,City,District,Street,Building,Apartament")] Registration registration)
        {
            Users currentUser = db.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();
            bool isAdmin = currentUser.Role_Id == 2;
            if (!isAdmin)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized); 
            }

            if (ModelState.IsValid)
            {
                db.Entry(registration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Passport_Id = new SelectList(db.Passport, "Passport_Id", "IssuedBy", registration.Passport_Id);
            return View(registration);
        }

        // GET: Registrations/Delete/5
        public ActionResult Delete(int? id)
        {
            Users currentUser = db.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();
            bool isAdmin = currentUser.Role_Id == 2;
            if (!isAdmin)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized); 
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = db.Registration.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        // POST: Registrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users currentUser = db.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();
            bool isAdmin = currentUser.Role_Id == 2;
            if (!isAdmin)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized); 
            }

            Registration registration = db.Registration.Find(id);
            db.Registration.Remove(registration);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
