using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Course_Project_TP_6.DAO;
using Course_Project_TP_6.Models;


namespace Course_Project_TP_6.Controllers
{   
    [Authorize]
    public class UsersController : Controller
    {
        UsersDAO usersDAO = new UsersDAO();

        private passportofficeEntities db = new passportofficeEntities();

        // GET: Users
        public ActionResult Index()
        {
            // var users = db.Users.Include(u => u.Gender).Include(u => u.Role);
            Users user = db.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();

            return View(user);
        }

        // GET: Users/Details/5
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
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            Users currentUser = db.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();
            bool isAdmin = currentUser.Role_Id == 2;
            if (!isAdmin)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized); 
            }

            ViewBag.Gender_Id = new SelectList(db.Gender, "Gender_Id", "Name");
            ViewBag.Role_Id = new SelectList(db.Role, "Role_Id", "Name");
            return View();
        }

        // POST: Users/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "User_Id,Role_Id,Gender_Id,UserName,UserLastName,UserPatronymic,CityOfBirth,Email,Password,PhoneNumber,UserDateOfBirth")] Users users)
        {
            Users currentUser = db.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();
            bool isAdmin = currentUser.Role_Id == 2;
            if (!isAdmin)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized); 
            }

            if (ModelState.IsValid)
            {
                db.Users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Gender_Id = new SelectList(db.Gender, "Gender_Id", "Name", users.Gender_Id);
            ViewBag.Role_Id = new SelectList(db.Role, "Role_Id", "Name", users.Role_Id);
            return View(users);
        }

        // GET: Users/Edit/5
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
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            ViewBag.Gender_Id = new SelectList(db.Gender, "Gender_Id", "Name", users.Gender_Id);
            ViewBag.Role_Id = new SelectList(db.Role, "Role_Id", "Name", users.Role_Id);
            return View(users);
        }

        // POST: Users/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "User_Id,Role_Id,Gender_Id,UserName,UserLastName,UserPatronymic,CityOfBirth,Email,Password,PhoneNumber,UserDateOfBirth")] Users users)
        {
            Users currentUser = db.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();
            bool isAdmin = currentUser.Role_Id == 2;
            if (!isAdmin)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized); 
            }

            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Gender_Id = new SelectList(db.Gender, "Gender_Id", "Name", users.Gender_Id);
            ViewBag.Role_Id = new SelectList(db.Role, "Role_Id", "Name", users.Role_Id);
            return View(users);
        }

        // GET: Users/Delete/5
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
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Delete/5
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

            Users users = db.Users.Find(id);
            db.Users.Remove(users);
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
