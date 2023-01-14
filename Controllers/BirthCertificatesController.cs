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
    public class BirthCertificatesController : Controller
    {
        private passportofficeEntities db = new passportofficeEntities();

        // GET: BirthCertificates
        public ActionResult Index()
        {
            Users currentUser = db.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();
            bool isAdmin = currentUser.Role_Id == 2;
            if (!isAdmin)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized); 
            }

            var birthCertificate = db.BirthCertificate.Include(b => b.Users);
            return View(birthCertificate.ToList());
        }

        // GET: BirthCertificates/Details/5
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
            BirthCertificate birthCertificate = db.BirthCertificate.Find(id);
            if (birthCertificate == null)
            {
                return HttpNotFound();
            }
            return View(birthCertificate);
        }

        // GET: BirthCertificates/Create
        public ActionResult Create()
        {
            Users currentUser = db.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();
            bool isAdmin = currentUser.Role_Id == 2;
            if (!isAdmin)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized); 
            }

            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "UserName");
            return View();
        }

        // POST: BirthCertificates/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BirthCertificate_Id,User_Id,DateOfIssue,IssuedBy,MothersFIO,FathersFIO,Number")] BirthCertificate birthCertificate)
        {
            Users currentUser = db.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();
            bool isAdmin = currentUser.Role_Id == 2;
            if (!isAdmin)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized); 
            }

            if (ModelState.IsValid)
            {
                db.BirthCertificate.Add(birthCertificate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "UserName", birthCertificate.User_Id);
            return View(birthCertificate);
        }

        // GET: BirthCertificates/Edit/5
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
            BirthCertificate birthCertificate = db.BirthCertificate.Find(id);
            if (birthCertificate == null)
            {
                return HttpNotFound();
            }
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "UserName", birthCertificate.User_Id);
            return View(birthCertificate);
        }

        // POST: BirthCertificates/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BirthCertificate_Id,User_Id,DateOfIssue,IssuedBy,MothersFIO,FathersFIO,Number")] BirthCertificate birthCertificate)
        {
            Users currentUser = db.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();
            bool isAdmin = currentUser.Role_Id == 2;
            if (!isAdmin)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized); 
            }

            if (ModelState.IsValid)
            {
                db.Entry(birthCertificate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "UserName", birthCertificate.User_Id);
            return View(birthCertificate);
        }

        // GET: BirthCertificates/Delete/5
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
            BirthCertificate birthCertificate = db.BirthCertificate.Find(id);
            if (birthCertificate == null)
            {
                return HttpNotFound();
            }
            return View(birthCertificate);
        }

        // POST: BirthCertificates/Delete/5
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

            BirthCertificate birthCertificate = db.BirthCertificate.Find(id);
            db.BirthCertificate.Remove(birthCertificate);
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
