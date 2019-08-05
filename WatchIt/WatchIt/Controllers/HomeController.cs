using Accord.MachineLearning.DecisionTrees.Learning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WatchIt.DAL;
using WatchIt.Models;

namespace WatchIt.Controllers
{
    public class HomeController : Controller
    {
        private WatchItContext db = new WatchItContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Register()
        {
            ViewBag.Message = "Reg Page";

            return View();
        }
        public ActionResult Movies()
        {
            ViewBag.Message = "This page contains all movies";

            return View();
        }

        public List<Movie> GetHomeMovies()
        {
            var movies = db.Movies.OrderByDescending(x => x.ReleaseDate).ToList();

            return movies;
        }

    }
}