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
                movie.Image = "/Images/movies/" + movie.Image + ".JPG";
                movie.Trailer = "/Trailers/" + movie.Trailer + ".mp4";
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
            
            // Remove '/Images/movies/' from the image name
            movie.Image = movie.Image.Remove(0, 15);

            // Remove '.JPG'
            movie.Image = movie.Image.Substring(0, movie.Image.Length - 4);

            // Remove '/Trailers/' from the trailer name
            movie.Trailer = movie.Trailer.Remove(0, 10);

            // Remove '.mp4'
            movie.Trailer = movie.Trailer.Substring(0, movie.Trailer.Length - 4);

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
                movie.Image = "/Images/movies/" + movie.Image + ".JPG";
                movie.Trailer = "/Trailers/" + movie.Trailer + ".mp4";
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
