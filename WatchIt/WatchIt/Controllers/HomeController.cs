using Accord.MachineLearning.DecisionTrees.Learning;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.MobileControls;
using System.Windows.Documents;
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
        public List<Order> GetCustomerOrders(int? customerID)
        {
            List<Order> orders = new List<Order>();
            var users = db.Customers.Where(s => s.CustomerID == customerID).ToList();
            if (users.Count() != 0)
            {
                if (users.First() != null)
                {
                    orders = users.First().Orders.ToList();
                }
            }
            return orders;

        }
            public List<Movie> GetMoviesByGender(int? customerGender, int? customerBirthYear)
        {
            const int NUMBER_OF_FEATURES = 2;

            var movies = db.Movies.ToList();

            int currGender;
            int currBirthYear;

            if (customerGender != null && customerBirthYear != null)
            {
                var orders = db.Orders.ToList();
                int year = 0;
                List<int[]> inputsList = new List<int[]>();
                List<int> outputsList = new List<int>();

                foreach (var order in orders)
                {
                    currGender = (int)order.Customer.Gender;
                    currBirthYear = order.Customer.BirthDate.Year;
                    year = currBirthYear;
                    foreach (var movie in order.Movies)
                    {
                        inputsList.Add(new int[NUMBER_OF_FEATURES] { currGender, currBirthYear });
                        outputsList.Add((int)movie.Genre);
                    }
                }
                inputsList.Add(new int[NUMBER_OF_FEATURES] { 0, year});
                outputsList.Add(0);

                int[][] inputs = inputsList.ToArray();
                int[] outputs = outputsList.ToArray();

                ID3Learning teacher = new ID3Learning();

                var tree = teacher.Learn(inputs, outputs);
                int predictedGenre = tree.Decide(new int[NUMBER_OF_FEATURES] { (int)customerGender, (int)customerBirthYear });

                movies = movies.Where(x => x.Genre == (Genre)predictedGenre).ToList();
            }
            return movies.ToList();
        }
    }
}