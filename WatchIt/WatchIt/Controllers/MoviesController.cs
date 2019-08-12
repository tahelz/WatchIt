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
            var movies = db.Movies.ToList();
            ViewBag.MaxPrice = db.Movies.Select(x => x.Price).Max();
            ViewBag.MinPrice = db.Movies.Select(x => x.Price).Min();
            return View(movies); 
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

            MovieDirectorView MovieDirector = GetDirector(id);
            return View(MovieDirector);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            ViewBag.Image = new SelectList(db.Movies, "Image", "Title");
            ViewBag.Trailer = new SelectList(db.Movies, "Trailer", "Title");
            ViewBag.DirectorID = new SelectList(db.Directors, "ID", "Name");
            return View();
        }
        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Description,Genre,Price,Image,DirectorID,Length,Rating,ReleaseDate,Trailer")] Movie movie)
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

            ViewBag.Image = new SelectList(db.Movies, "Image", "Title");
            ViewBag.Trailer = new SelectList(db.Movies, "Trailer", "Title");

            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Description,Genre,Price,Image,DirectorID,Length,Rating,ReleaseDate,Trailer")] Movie movie)
        {
            if (ModelState.IsValid)
            {   
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

            MovieDirectorView MovieDirector = GetDirector(id);
            return View(MovieDirector);
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
            var movies = db.Movies.ToList();
            
            for(var x = 0; x < movies.Count(); x++)
            {
                movies[x].Title = movies[x].Title.ToLower();
            }

            MovieName.ToLower();
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

            for (var x = 0; x < movies.Count(); x++)
            {
                movies[x].Title = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(movies[x].Title.ToLower());
            }

            ViewBag.MaxPrice = db.Movies.Select(x => x.Price).Max();
            ViewBag.MinPrice = db.Movies.Select(x => x.Price).Min();
            ViewBag.CurrentPrice = Price;
            return View(movies.ToList());
        }
        public MovieDirectorView GetDirector(int? MovieId)
        {
            var ChosenMovie = from m in db.Movies
                              join d in db.Directors
                                on m.DirectorID equals d.ID
                              where m.ID == MovieId
                              select new MovieDirectorView
                              {
                                  MovieID = m.ID,
                                  Title = m.Title,
                                  Description = m.Description,
                                  DirectorName = d.Name,
                                  Genre = m.Genre,
                                  Image = m.Image,
                                  Length = m.Length,
                                  Price = m.Price,
                                  Rating = m.Rating,
                                  ReleaseDate = m.ReleaseDate,
                                  Trailer = m.Trailer,
                                  DirectorID = d.ID
                              };

            return ChosenMovie.First();
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
