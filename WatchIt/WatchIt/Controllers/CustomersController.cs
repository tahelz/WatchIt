using System.Collections.Generic;
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
            return View(db.Customers.Where(x => x.IsAdmin == false).ToList());
        }

        [HttpPost]
        public ActionResult Index(string FirstName, string LastName, string City, Gender? gender)
        {
            var customers = db.Customers.Where(x => x.FirstName != "admin").ToList();

            for (var x = 0; x < customers.Count(); x++)
            {
                customers[x].FirstName = customers[x].FirstName.ToLower();
                customers[x].LastName = customers[x].LastName.ToLower();
                customers[x].City = customers[x].City.ToLower();
            }

            if (!string.IsNullOrEmpty(FirstName))
            {
                customers = customers.Where(x => x.FirstName.Contains(FirstName)).ToList();
            }

            if (!string.IsNullOrEmpty(LastName))
            {
                customers = customers.Where(x => x.LastName.Contains(LastName)).ToList();
            }

            if (!string.IsNullOrEmpty(City))
            {
                customers = customers.Where(x => x.City.Contains(City)).ToList();
            }

            if (gender != null)
            {
                customers = customers.Where(x => x.Gender == gender).ToList();
            }
            

            for (var x = 0; x < customers.Count(); x++)
            {
                customers[x].FirstName = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(customers[x].FirstName.ToLower());
                customers[x].LastName = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(customers[x].LastName.ToLower());
                customers[x].City = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(customers[x].City.ToLower());
            }

            return View(customers.ToList());
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
        public ActionResult Create([Bind(Include = "CustomerID,FirstName,LastName,BirthDate,Gender,Email,Password,City,Street")] Customer customer)
        {
            if (db.Customers.Where(c => c.Email == customer.Email).Count() > 0)
            {
                ViewBag.ErrMsg = "Email adress already exists.";
            }
            else
            {
                List<Order> orders = new List<Order>();
                customer.Orders = orders;

                db.Customers.Add(customer);
                db.SaveChanges();
                System.Web.HttpContext.Current.Session["Customer"] = customer;
                return RedirectToAction("Index", "Home");
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
        public ActionResult Edit([Bind(Include = "CustomerID,FirstName,LastName,BirthDate,Gender,Email,Password,City,Street")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                System.Web.HttpContext.Current.Session["Customer"] = customer;
                return RedirectToAction("Details");
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
            var user = db.Customers.Where(s => s.Email == email &&
                                               s.Password == password).ToList();
            if (user.Count() != 0)
            {
                var existingUser = user.First();

                if (existingUser != null)
                {
                    System.Web.HttpContext.Current.Session["Customer"] = existingUser;

                    return RedirectToAction("Index", "Home");

                }
            }

            ViewBag.ErrMsg = "Incorrect username or password";
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

        public ActionResult CustomersByBranch(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var CustomersByBranch = from c in db.Customers
                                    join b in db.Branches on
                                        c.Orders.Select(x => x.Branch).Where(y => y.BranchID == id).FirstOrDefault().BranchID equals b.BranchID
                                    where b.BranchID == id
                                    select new BranchCustomerView
                                    {
                                        BranchId = b.BranchID,
                                        branchName = b.BranchName,
                                        branchCity = b.BranchCity,
                                        firstName = c.FirstName,
                                        lastName = c.LastName,
                                        birthDate = c.BirthDate,
                                        CustomerID = c.CustomerID
                                    };

            ViewBag.BranchName = CustomersByBranch.First().branchName;
            return View(CustomersByBranch.ToList().Distinct());
        }
    }
}
