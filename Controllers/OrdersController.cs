using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Course_Project_TP_6.Models;


namespace Course_Project_TP_6.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private passportofficeEntities db = new passportofficeEntities();

        // GET: Orders
        public ActionResult Index()
        {
            Users currentUser = db.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();
            bool isAdmin = currentUser.Role_Id == 2;

            IQueryable<Orders> orders;  
            if (!isAdmin)
            {
                orders = db.Orders.Include(o => o.OrderType)
                    .Include(o => o.OrderStatus)
                    .Include(o => o.Users)
                    .Where(x => x.User_Id == currentUser.User_Id);
            }
            else 
            {
                orders = db.Orders.Include(o => o.OrderType)
                    .Include(o => o.OrderStatus)
                    .Include(o => o.Users)
                    .OrderBy(p => p.Status_Id);
            }

            ViewBag.IsAdmin = isAdmin;
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }

            Users currentUser = db.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();
            bool isAdmin = currentUser.Role_Id == 2;
            if (!isAdmin && orders.User_Id != currentUser.User_Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized); 
            }

            return View(orders);
        }

        // GET: Orders/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.OrderType_Id = new SelectList(db.OrderType, "OrderType_Id", "Name");
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Order_Id,User_Id,Status_Id,OrderType_Id,OrderName,CreationDate")] Orders orders)
        {
            Users currentUser = db.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();
            orders.User_Id = currentUser.User_Id;
            orders.Status_Id = 1;
            orders.CreationDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Orders.Add(orders);
                try 
                {
                    db.SaveChanges();
                    return RedirectToAction("Poshlina");
                }
                catch (Exception ex) 
                {
                    Debug.WriteLine($"<Register()> Ошибка при добавлении записи: {ex}");
                    ViewBag.OrderType_Id = new SelectList(db.OrderType, "OrderType_Id", "Name", orders.OrderType_Id);
                    return View(orders);
                }
            }

            ViewBag.OrderType_Id = new SelectList(db.OrderType, "OrderType_Id", "Name", orders.OrderType_Id);
            return View(orders);
        }

        // GET: Orders/Poshlina
        [Authorize]
        public ActionResult Poshlina()
        {
            return View();
        }

        // POST: Orders/Poshlina
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Poshlina(CreditCard card)
        {
            return RedirectToAction("Index");
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }

            Users currentUser = db.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();
            bool isAdmin = currentUser.Role_Id == 2;
            if (!isAdmin)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized); 
            }

            ViewBag.OrderType_Id = new SelectList(db.OrderType, "OrderType_Id", "Name", orders.OrderType_Id);
            ViewBag.Status_Id = new SelectList(db.OrderStatus, "Status_Id", "Name", orders.Status_Id);
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "UserName", orders.User_Id);
            return View(orders);
        }

        // POST: Orders/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Order_Id,User_Id,Status_Id,OrderType_Id,OrderName,CreationDate")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrderType_Id = new SelectList(db.OrderType, "OrderType_Id", "Name", orders.OrderType_Id);
            ViewBag.Status_Id = new SelectList(db.OrderStatus, "Status_Id", "Name", orders.Status_Id);
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "UserName", orders.User_Id);
            return View(orders);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }

            Users currentUser = db.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();
            bool isAdmin = currentUser.Role_Id == 2;
            if (!isAdmin && orders.User_Id != currentUser.User_Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized); 
            }

            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Orders orders = db.Orders.Find(id);
            db.Orders.Remove(orders);
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
