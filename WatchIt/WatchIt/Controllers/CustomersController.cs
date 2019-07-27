using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WatchIt.DAL;
using WatchIt.Models;

namespace WatchIt.Controllers
{
    public class CustomersController : Controller
    {
        private WatchItContext db = new WatchItContext();

        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
       {
            Customer CurrentCustomer;

            if (id == null)
            {
                var customer = System.Web.HttpContext.Current.Session["Customer"];
                if (customer == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                return View(customer);
            }
            else
            {
                CurrentCustomer = db.Customers.Find(id);
                if (CurrentCustomer == null)
                {
                    return HttpNotFound();
                }
            }

        return View(CurrentCustomer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,FirstName,LastName,Gender,Email,Password,City,Street,PhoneNumber,IsAdmin")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,FirstName,LastName,Gender,Email,Password,City,Street,PhoneNumber,IsAdmin")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Home/indedx");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            var users = db.Customers.ToList();
            var existingUser = db.Customers.Where(s => s.Email == email &&
                                                    s.Password == password).ToList().First();

            if (existingUser != null)
            {
                System.Web.HttpContext.Current.Session["Customer"] = existingUser;
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMsg = "Incorrect username or password";
            return View();
        }

        public ActionResult Logoff()
        {
            System.Web.HttpContext.Current.Session["Customer"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult GetFirstName(string term)
        {
            var firstNames = (from p in db.Customers where p.FirstName.Contains(term) select p.FirstName).Distinct().Take(10);

            return Json(firstNames, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLastName(string term)
        {
            var lastNames = (from p in db.Customers where p.LastName.Contains(term) select p.LastName).Distinct().Take(10);

            return Json(lastNames, JsonRequestBehavior.AllowGet);
        }
    }
}
