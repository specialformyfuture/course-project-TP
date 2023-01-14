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
    public class SNILSController : Controller
    {
        private passportofficeEntities db = new passportofficeEntities();

        // GET: SNILS
        public ActionResult Index()
        {
            Users currentUser = db.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();
            bool isAdmin = currentUser.Role_Id == 2;
            if (!isAdmin)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized); 
            }

            var sNILS = db.SNILS.Include(s => s.Users);
            return View(sNILS.ToList());
        }

        // GET: SNILS/Details/5
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
            SNILS sNILS = db.SNILS.Find(id);
            if (sNILS == null)
            {
                return HttpNotFound();
            }
            return View(sNILS);
        }

        // GET: SNILS/Create
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

        // POST: SNILS/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SNILS_Id,User_Id,Number")] SNILS sNILS)
        {
            Users currentUser = db.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();
            bool isAdmin = currentUser.Role_Id == 2;
            if (!isAdmin)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized); 
            }

            if (ModelState.IsValid)
            {
                db.SNILS.Add(sNILS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "UserName", sNILS.User_Id);
            return View(sNILS);
        }

        // GET: SNILS/Edit/5
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
            SNILS sNILS = db.SNILS.Find(id);
            if (sNILS == null)
            {
                return HttpNotFound();
            }
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "UserName", sNILS.User_Id);
            return View(sNILS);
        }

        // POST: SNILS/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SNILS_Id,User_Id,Number")] SNILS sNILS)
        {
            Users currentUser = db.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();
            bool isAdmin = currentUser.Role_Id == 2;
            if (!isAdmin)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized); 
            }

            if (ModelState.IsValid)
            {
                db.Entry(sNILS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "UserName", sNILS.User_Id);
            return View(sNILS);
        }

        // GET: SNILS/Delete/5
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
            SNILS sNILS = db.SNILS.Find(id);
            if (sNILS == null)
            {
                return HttpNotFound();
            }
            return View(sNILS);
        }

        // POST: SNILS/Delete/5
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

            SNILS sNILS = db.SNILS.Find(id);
            db.SNILS.Remove(sNILS);
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
