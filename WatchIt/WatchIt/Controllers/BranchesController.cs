using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WatchIt.DAL;
using WatchIt.Models;

namespace WatchIt.Controllers
{
    public class BranchesController : Controller
    {
        private WatchItContext db = new WatchItContext();

        // GET: Branches
        public ActionResult Index()
        {
            var x = db.Branches.ToList();
            return View(db.Branches.ToList());
        }

        [HttpPost]
        public ActionResult Index(string BranchName, string City, string Phone)
        {
            var branches = db.Branches.ToList();

            for (var x = 0; x < branches.Count(); x++)
            {
                branches[x].BranchName = branches[x].BranchName.ToLower();
                branches[x].BranchCity = branches[x].BranchCity.ToLower();
            }

            if (!string.IsNullOrEmpty(BranchName))
            {
                branches = branches.Where(x => x.BranchName.Contains(BranchName)).ToList();
            }

            if (!string.IsNullOrEmpty(City))
            {
                branches = branches.Where(x => x.BranchCity.Contains(City)).ToList();
            }

            if (!string.IsNullOrEmpty(Phone))
            {
                branches = branches.Where(x => x.BranchsPhoneNumber == Phone).ToList();
            }

            for (var x = 0; x < branches.Count(); x++)
            {
                branches[x].BranchName = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(branches[x].BranchName.ToLower());
                branches[x].BranchCity = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(branches[x].BranchCity.ToLower());
            }

            return View(branches.ToList());
        }


        // GET: Branch
        public Branch GetFirstBranch()
        {
            return db.Branches.ToList().First();
        }

        public List<Branch> GetOtherBranches(int branchId)
        {
            return db.Branches.ToList().Where(x => x.BranchID != branchId).ToList();
        }

        public List<Branch> GetAllBranches()
        {
            return db.Branches.ToList();
        }

        public Branch GetBranchById(int branchId)
        {
            return db.Branches.Find(branchId);
        }

        // GET: Branches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        // GET: Branches/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Branches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BranchID,BranchName,BranchCity,BranchStreet,BranchsPhoneNumber,BranchLat,BranchLng")] Branch branch)
        {
            if (ModelState.IsValid)
            {
                db.Branches.Add(branch);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(branch);
        }

        // GET: Branches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        // POST: Branches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BranchID,BranchName,BranchCity,BranchStreet,BranchsPhoneNumber,BranchLat,BranchLng")] Branch branch)
        {
            if (ModelState.IsValid)
            {
                db.Entry(branch).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(branch);
        }

        // GET: Branches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        // POST: Branches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Branch branch = db.Branches.Find(id);
            var orders = db.Orders.Where(x => x.BranchID == id).ToList();
            for (var x = 0; x < orders.Count(); x++)
            {
                orders[x].Movies = null;
                orders[x].TotalPrice = 0;
            }
            db.Branches.Remove(branch);
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
