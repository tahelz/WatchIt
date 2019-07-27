using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WatchIt.DAL;
using WatchIt.Models;

namespace WatchIt.Controllers
{
    public class MoviesController : Controller
    {
        private WatchItContext db = new WatchItContext();

        // GET: Movies
        public ActionResult Index()
        {
            var temp = db.Movies.ToList();
            var movies = db.Movies.Include(d => d.Director);
            
            // var movies = db.Movies.ToList();
            return View(movies.ToList());
        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }

            var entryPoint = (from m in db.Movies
                              join d in db.Directors
                                on m.DirectorID equals d.ID
                              where m.ID == id 
                              select new
                              {
                                id = m.ID,
                                title = m.Title,
                                Description = m.Description,
                                
                              }).Take(10).ToList();

            //   Director director = db.Directors.Find(movie.DirectorID);
            //   movie.Director = director;

            //var client = new RestClient("http://free.currencyconverterapi.com/api/v3/convert?q=USD_ILS&compact=y");
            //String json = client.MakeRequest();

            //Money money = JsonConvert.DeserializeObject<Money>(json);

            //movie.Price = movie.Price * money.usd_ils.val;

            return View(movie);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            ViewBag.DirectorID = new SelectList(db.Directors, "ID", "Name");
            return View();
        }
        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Description,Genre,Price,Image,DirectorID,Length,Rating,ReleaseDate")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DirectorID = new SelectList(db.Directors, "ID", "Name", movie.DirectorID);
            return View(movie);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            ViewBag.DirectorID = new SelectList(db.Directors, "ID", "Name", movie.DirectorID);
            //ViewBag.image = movie.Image;
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Description,Genre,Price,Image,DirectorID,Length,Rating,ReleaseDate")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                //movie.Image = "/Images/movies/" + movie.Image;
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DirectorID = new SelectList(db.Directors, "ID", "Name", movie.DirectorID);
            return View(movie);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Index(int? Price, string MovieName, WatchIt.Models.Genre? Genere)
        {
            // var movies = from a in db.Movies select a;
            
            var movies = db.Movies.ToList();

            if (!string.IsNullOrEmpty(MovieName))
            {
                movies = movies.Where(x => x.Title.Contains(MovieName)).ToList();
            }
            if (Price != null)
            {
                movies = movies.Where(x => x.Price <= Price).ToList();
            }
            if (Genere != null)
            {
                movies = movies.Where(x => x.Genre == Genere).ToList();
            }

            ViewBag.MaxPrice = db.Movies.Select(x => x.Price).Max();
            return View(movies.ToList());
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
