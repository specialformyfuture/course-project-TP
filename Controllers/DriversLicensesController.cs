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
    public class DriversLicensesController : Controller
    {
        private passportofficeEntities db = new passportofficeEntities();

        // GET: DriversLicenses
        public ActionResult Index()
        {
            Users currentUser = db.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();
            bool isAdmin = currentUser.Role_Id == 2;
            if (!isAdmin)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized); 
            }

            var driversLicense = db.DriversLicense.Include(d => d.Category).Include(d => d.Users);
            return View(driversLicense.ToList());
        }

        // GET: DriversLicenses/Details/5
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
            DriversLicense driversLicense = db.DriversLicense.Find(id);
            if (driversLicense == null)
            {
                return HttpNotFound();
            }
            return View(driversLicense);
        }

        // GET: DriversLicenses/Create
        public ActionResult Create()
        {
            Users currentUser = db.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();
            bool isAdmin = currentUser.Role_Id == 2;
            if (!isAdmin)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized); 
            }

            ViewBag.Category_Id = new SelectList(db.Category, "Category_Id", "Name");
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "UserName");
            return View();
        }

        // POST: DriversLicenses/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DriversLicense_Id,User_Id,Category_Id,Name,DateOfIssue,ExpiringDate,CityOfIssue,Number")] DriversLicense driversLicense)
        {
            Users currentUser = db.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();
            bool isAdmin = currentUser.Role_Id == 2;
            if (!isAdmin)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized); 
            }

            driversLicense.Name = "1";
            if (ModelState.IsValid)
            {
                db.DriversLicense.Add(driversLicense);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Category_Id = new SelectList(db.Category, "Category_Id", "Name", driversLicense.Category_Id);
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "UserName", driversLicense.User_Id);
            return View(driversLicense);
        }

        // GET: DriversLicenses/Edit/5
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
            DriversLicense driversLicense = db.DriversLicense.Find(id);
            if (driversLicense == null)
            {
                return HttpNotFound();
            }
            ViewBag.Category_Id = new SelectList(db.Category, "Category_Id", "Name", driversLicense.Category_Id);
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "UserName", driversLicense.User_Id);
            return View(driversLicense);
        }

        // POST: DriversLicenses/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DriversLicense_Id,User_Id,Category_Id,Name,DateOfIssue,ExpiringDate,CityOfIssue,Number")] DriversLicense driversLicense)
        {
            Users currentUser = db.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();
            bool isAdmin = currentUser.Role_Id == 2;
            if (!isAdmin)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized); 
            }

            if (ModelState.IsValid)
            {
                db.Entry(driversLicense).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Category_Id = new SelectList(db.Category, "Category_Id", "Name", driversLicense.Category_Id);
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "UserName", driversLicense.User_Id);
            return View(driversLicense);
        }

        // GET: DriversLicenses/Delete/5
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
            DriversLicense driversLicense = db.DriversLicense.Find(id);
            if (driversLicense == null)
            {
                return HttpNotFound();
            }
            return View(driversLicense);
        }

        // POST: DriversLicenses/Delete/5
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

            DriversLicense driversLicense = db.DriversLicense.Find(id);
            db.DriversLicense.Remove(driversLicense);
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
